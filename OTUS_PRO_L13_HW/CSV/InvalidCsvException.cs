namespace OTUS_PRO_L13_HW.CSV
{
    [Serializable]
    internal class InvalidCsvException : Exception
    {
        private Exception ex;

        public InvalidCsvException()
        {
        }

        public InvalidCsvException(Exception ex)
        {
            this.ex = ex;
        }

        public InvalidCsvException(string? message) : base(message)
        {
        }

        public InvalidCsvException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}