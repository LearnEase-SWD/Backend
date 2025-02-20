namespace LearnEase.Core.Utils
{
    [AttributeUsage(AttributeTargets.All)]
    public class ResponseName : Attribute
    {
        public string Name { get; set; }
        public ResponseName(string name)
        {
            Name = name;
        }
    }
}