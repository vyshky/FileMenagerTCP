namespace Model
{
    public class PackageModel
    {
        private Dictionary<string, Action<Package>> dictionary;      
        public PackageModel()
        {
            dictionary = new Dictionary<string, Action<Package>>();
            dictionary.Add("get", new Action<Package>(package => Get(package)));
            dictionary.Add("post", new Action<Package>(package => Post(package)));            
        }
        public void Execute(Package _package)
        {
            _ = _package ?? throw new ArgumentNullException("Ошибка при открытии пакета");
            Action<Package> execMethod = dictionary[_package.Method];
            execMethod(_package);
        }
        
        public static void Get(Package package)
        {
            Console.WriteLine("GET method вызван");
            
        }
        public static void Post(Package package)
        {
            Console.WriteLine("POST method вызван");
        }
    }
}
