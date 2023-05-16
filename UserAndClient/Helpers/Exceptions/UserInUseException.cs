using System.Runtime.Serialization;

namespace UserAndClient.Helpers.Exceptions
{

        [Serializable]
        public class UserInUseException : Exception
        {
            public UserInUseException(string message)
                : base(message)
            {
            }

            protected UserInUseException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                if (info == null)
                    throw new ArgumentNullException(nameof(info));
            }

            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                if (info == null)
                    throw new ArgumentNullException(nameof(info));

                base.GetObjectData(info, context);
            }
        }
    
}
