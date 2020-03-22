using CashRegisterAPI.Data;
using CashRegisterAPI.Data.DataModels;
using CashRegisterAPI.Services.Interfaces;
using CashRegisterAPI.Services.Models;
using CashRegisterAPI.Utilities;
using CashRegisterAPI.Utilities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CashRegisterAPI.Services
{
    public class CashRegisterService : ICashRegisterService
    {
        private readonly CashRegisterContext context;

        public CashRegisterService(CashRegisterContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GeneralResponseModel> Deposit(BanknotesDTO banknotesInput)
        {
            if (banknotesInput == null)
            {
                return Validation.ValidateReponse(false, (int)HttpStatusCode.Forbidden, FailureMessage: Messages.NoCashIsProvidedToBeDeposited);
            }


            var isRegisterInitialized = await this.context.Banknotes.AnyAsync();
            if (isRegisterInitialized == false)
            {
                await this.context.Banknotes.AddAsync(new Banknotes());
                await this.context.SaveChangesAsync();
            }

            var banknotes = await this.context.Banknotes.FirstOrDefaultAsync();
            banknotes.Fifty += banknotesInput.Fifty;
            banknotes.Twenty += banknotesInput.Twenty;
            banknotes.Ten += banknotesInput.Ten;
            banknotes.Five += banknotesInput.Five;
            banknotes.Two += banknotesInput.Two;
            banknotes.One += banknotesInput.One;

            await this.context.SaveChangesAsync();

            return Validation.ValidateReponse(true, (int)HttpStatusCode.OK, SuccessfulMessage: Messages.DepositPassed);
        }

        public async Task<GeneralResponseModel<BanknotesDTO>> TotalAmountAvailable()
        {
            var banknotesDTO = await this.context.Banknotes.Select(b => new BanknotesDTO
            {
                Fifty = b.Fifty,
                Twenty = b.Twenty,
                Ten = b.Ten,
                Five = b.Five,
                Two = b.Two,
                One = b.One
            }).FirstOrDefaultAsync();

            if (banknotesDTO == null)
            {
                return Validation.ValidateResponse(false, (int)HttpStatusCode.Forbidden, Model: banknotesDTO, FailureMessage: Messages.CashRegisterEmpty);
            }

            return Validation.ValidateResponse(true, (int)HttpStatusCode.OK, Model: banknotesDTO);
        }

        public async Task<GeneralResponseModel<BanknotesDTO>> Withdraw(double withdrawAmount)
        {
            var amount = RoundUpAmount(withdrawAmount);
            return await HandleCashWithdraw(amount);
        }

        public async Task<GeneralResponseModel<BanknotesDTO>> CalculateChange(double priceInput, double sumInput)
        {
            var amount = RoundUpAmount(sumInput - priceInput);
            return await HandleCashWithdraw(amount);
        }

        public async Task<GeneralResponseModel<BanknotesDTO>> HandleCashWithdraw(int withdrawAmount)
        {
            if (withdrawAmount < 0)
            {
                return Validation.ValidateResponse(false, (int)HttpStatusCode.Forbidden, Model: new BanknotesDTO(), FailureMessage: Messages.NegativeAmountCannotBeWithdrawn);
            }

            var banknotes = await this.context.Banknotes.FirstOrDefaultAsync();

            if (banknotes == null)
            {
                return Validation.ValidateResponse(false, (int)HttpStatusCode.Forbidden, Model: new BanknotesDTO(), FailureMessage: Messages.CashRegisterEmpty);
            }

            Dictionary<int, Dictionary<int, int>> notesDic = new Dictionary<int, Dictionary<int, int>>();

            AddNotesToList(0, 50, banknotes.Fifty, ref notesDic);
            AddNotesToList(1, 20, banknotes.Twenty, ref notesDic);
            AddNotesToList(2, 10, banknotes.Ten, ref notesDic);
            AddNotesToList(3, 5, banknotes.Five, ref notesDic);
            AddNotesToList(4, 2, banknotes.Two, ref notesDic);
            AddNotesToList(5, 1, banknotes.One, ref notesDic);

            var notesSum = notesDic.Values.Sum(x => x.Keys.Sum() * x.Values.Sum());

            if (notesSum < withdrawAmount)
            {
                return Validation.ValidateResponse(false, (int)HttpStatusCode.Forbidden, Model: new BanknotesDTO(), FailureMessage: Messages.CashNotEnoughToWithdraw);
            }

            Dictionary<int, int> notesReturnedCountDic = new Dictionary<int, int>()
            {
                {50, 0 }, {20, 0 }, {10, 0 }, {5, 0 }, {2, 0 }, {1, 0 }
            };


            for (int i = 0; i < Constraints.BanknotesTypeAmount; i++)
            {
                var currentNoteKey = notesDic[i].Keys.FirstOrDefault();
                var currentNoteValueCount = notesDic[i].Values.FirstOrDefault();
                var notesUsed = 0;
                if (i > notesDic.Count - 1)
                {
                    break;
                }

                // if currentNoteValueCount == 0, that means we have no banknotes of this kind available in the register therefore the loop shall not continue
                if (currentNoteValueCount <= 0)
                {
                    continue;
                }

                if (withdrawAmount >= currentNoteKey)
                {
                    while (currentNoteValueCount > 0 && withdrawAmount > 0 && withdrawAmount >= currentNoteKey)
                    {
                        withdrawAmount = withdrawAmount - currentNoteKey;
                        currentNoteValueCount--;
                        notesUsed++;

                    }

                    notesReturnedCountDic[currentNoteKey] = notesUsed;
                }
            }

            if (withdrawAmount > 0)
            {
                return Validation.ValidateResponse(false, (int)HttpStatusCode.Forbidden, Model: new BanknotesDTO(), FailureMessage: Messages.BanknotesCannotFulfilPayment);
            }

            var banknotesDTO = new BanknotesDTO
            {
                Fifty = notesReturnedCountDic[50],
                Twenty = notesReturnedCountDic[20],
                Ten = notesReturnedCountDic[10],
                Five = notesReturnedCountDic[5],
                Two = notesReturnedCountDic[2],
                One = notesReturnedCountDic[1],
            };

            banknotes.Fifty -= notesReturnedCountDic[50];
            banknotes.Twenty -= notesReturnedCountDic[20];
            banknotes.Ten -= notesReturnedCountDic[10];
            banknotes.Five -= notesReturnedCountDic[5];
            banknotes.Two -= notesReturnedCountDic[2];
            banknotes.One -= notesReturnedCountDic[1];

            await this.context.SaveChangesAsync();

            return Validation.ValidateResponse(true, (int)HttpStatusCode.OK, Model: banknotesDTO);
        }

        public IDictionary<int, Dictionary<int, int>> AddNotesToList(int index, int noteValue, int notesCount, ref Dictionary<int, Dictionary<int, int>> currentNotesDic)
        {
            currentNotesDic.Add(index, new Dictionary<int, int>());

            currentNotesDic[index].Add(noteValue, notesCount);

            return currentNotesDic;
        }

        public int RoundUpAmount(double amount)
        {
            return (int)Math.Round(amount);
        }
    }
}
