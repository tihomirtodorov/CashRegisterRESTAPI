namespace CashRegisterAPI.Utilities
{
    public static class Messages
    {
        public static string NoCashIsProvidedToBeDeposited => "No cash provided therefore your deposit has failed!";

        public static string DepositPassed => "Deposit successful!";

        public static string CashRegisterEmpty => "Cash register is empty!";

        public static string CashNotEnoughToWithdraw => "There is not enough cash in the cash register to complete the transaction!";

        public static string BanknotesCannotFulfilPayment => "Banknotes in the cash register cannot fulfil the payment!";

        public static string NegativeAmountCannotBeWithdrawn => "Request has failed because the input given contains negative numbers";
    }
}
