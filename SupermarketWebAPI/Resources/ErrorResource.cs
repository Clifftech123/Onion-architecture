namespace SupermarketWebAPI.Resources
{
    public class ErrorResource
    {
        public bool Success = false;
        public List<string> Messages { get; set; }


        // Constructor that initializes the Messages property
        public ErrorResource(List<string> messages)
        {
            Messages = messages ?? [];
        }


        // Constructor that initializes the Messages property
        public ErrorResource(string message)
        {
            Messages = [];

            if (!string.IsNullOrWhiteSpace(message))
            {
                this.Messages.Add(message);
            }
        }


    }
}
