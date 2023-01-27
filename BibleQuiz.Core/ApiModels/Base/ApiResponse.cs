namespace BibleQuiz.Core
{
    /// <summary>
    /// The api response model oject for an api request
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// If the request was successful
        /// </summary>
        public bool Successful => ErrorMessage == null;

        /// <summary>
        /// If there is any error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The result from the api call
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// Warning result from the api call if any
        /// </summary>
        public object WarningResult { get; set; }

        /// <summary>
        /// Error result from the api call if any
        /// </summary>
        public object ErrorResult { get; set; }
    }

    /// <summary>
    /// The generic response object type of an api call
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TWarningResult"></typeparam>
    /// <typeparam name="TErrorResult"></typeparam>
    public class ApiResponse<TResult, TWarningResult, TErrorResult> : ApiResponse
    {
        /// <summary>
        /// The TResult from the api request
        /// </summary>
        public new TResult Result { get => (TResult)base.Result; set => base.Result = value; }

        /// <summary>
        /// The TWarningResult from the api request
        /// </summary>
        public new TWarningResult WarningResult { get => (TWarningResult)base.WarningResult; set => base.WarningResult = value; }

        /// <summary>
        /// The TErrorResult from the api request
        /// </summary>
        public new TErrorResult ErrorResult { get => (TErrorResult)base.ErrorResult; set=> base.ErrorResult = value; }

    }
}

