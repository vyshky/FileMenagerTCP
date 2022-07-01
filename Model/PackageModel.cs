namespace Model
{
    public class PackageModel
    {
        private Dictionary<string, Action> dictionary = new Dictionary<string, Action>();
        private Package package = new Package();
      
        public PackageModel()
        {
            dictionary.Add("get", Get);
            dictionary.Add("post", Post);
        }
        public void Execute(Package _package)
        {
            package = _package ?? throw new ArgumentNullException("");
            Action execMethod = dictionary[package.Method];
            execMethod();
        }

        public void Get()
        {
            Console.WriteLine("GET method вызван");
        }
        public void Post()
        {
            Console.WriteLine("POST method вызван");
        }
    }
}
