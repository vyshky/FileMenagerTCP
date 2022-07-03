namespace Model
{
    public class PackageModel
    {
        private static Dictionary<string, Action<Package>> dictionary = new Dictionary<string, Action<Package>>();       
        public PackageModel()
        {
            dictionary.Add("get", new Action<Package>(package => Get(package)));
            dictionary.Add("post", new Action<Package>(package => Post(package)));            
        }
        public static void Execute(Package _package)
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
