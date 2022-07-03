namespace Model
{
    public class PackageModel
    {
        private Dictionary<string, Action<Package>> dictionary = new Dictionary<string, Action<Package>>();
        private Package package = new Package();

        public PackageModel()
        {
            dictionary.Add("get", new Action<Package>(package => Get(package)));
            dictionary.Add("post", new Action<Package>(package => Post(package)));            
        }
        public void Execute(Package _package)
        {
            _ = _package ?? throw new ArgumentNullException("Ошибка при открытии пакета");
            Action<Package> execMethod = dictionary[package.Method];
            execMethod(_package);
        }
        
        public void Get(Package package)
        {
            Console.WriteLine("GET method вызван");
            
        }
        public void Post(Package package)
        {
            Console.WriteLine("POST method вызван");
        }
    }
}
