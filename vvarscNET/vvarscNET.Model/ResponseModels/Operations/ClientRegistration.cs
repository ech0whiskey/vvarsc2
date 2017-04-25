namespace vvarscNET.Model.ResponseModels.Operations
{
    /// <summary>
    /// An Object to track Registration of Verified Prolifiq Clients.
    /// </summary>
    public class ClientRegistration
    {
        /// <summary>
        /// Friendly Name for the ClientApp. Enforced Unique in Database
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// Private Key for ClientApp
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// Success ReturnURL
        /// </summary>
        public string ReturnURL { get; set; }

        /// <summary>
        /// Return URL for Error re-directing
        /// </summary>
        public string ErrorReturnURL { get; set; }

        /// <summary>
        /// Is this object active or inactive.
        /// </summary>
        public bool? IsActive { get; set; }

        public ClientRegistration()
        {
            IsActive = true;
        }
    }
}
