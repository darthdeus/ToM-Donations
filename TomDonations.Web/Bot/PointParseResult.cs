namespace TomDonations.Web {
    public class PointParseResult {
        public bool IsOk { get; set; }
        public int? Amount { get; set; }
        public string ErrorMessage { get; set; }

        private PointParseResult(bool success, int? amount, string error) {
            IsOk = success;
            Amount = amount;
            ErrorMessage = error;
        }

        public static PointParseResult Success(int amount) {
            return new PointParseResult(true, amount, null);
        }

        public static PointParseResult Error(string error) {
            return new PointParseResult(false, null, error);
        }
    }
}