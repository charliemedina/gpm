using System.Runtime.Serialization;

namespace Application.Commands
{
    [DataContract]
    public class CommandResult
    {
        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public string Identity { get; set; }

        [DataMember]
        public string Error { get; set; }

        public CommandResult(bool isSuccess, object identity, string errorMessage)
        {
            IsSuccess = isSuccess;
            Identity = identity?.ToString();
            Error = errorMessage;
        }

        public CommandResult(bool isSuccess, object identity) : this(isSuccess, identity, string.Empty) { }
        public CommandResult(string errorMessage) : this(false, string.Empty, errorMessage) { }
        public CommandResult() { IsSuccess = true; }
    }

    [DataContract]
    public class CommandResult<T> : CommandResult
    {
        [DataMember]
        public T Item { get; set; }

        public CommandResult(bool isSuccess, object identity, string errorMessage, T item) : base(isSuccess, identity, errorMessage)
        {
            Item = item;
        }

        public CommandResult(bool isSuccess, object identity, T item) : this(isSuccess, identity, string.Empty, item) { }
        public CommandResult(string errorMessage, T item) : this(false, string.Empty, errorMessage, item) { }
        public CommandResult(T item) : this(true, string.Empty, string.Empty, item) { }
        public CommandResult() : this(false, string.Empty, string.Empty, default(T)) { }
    }
}