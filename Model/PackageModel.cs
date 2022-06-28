namespace Model
{
    public class PackageModel
    {

        private static Dictionary<string, Action> dictionary = new Dictionary<string, Action>();

        private static void Init()
        {
            dictionary.Add("get", Get);
            dictionary.Add("post", Post);
        }

        public void Create()
        {
            //value = value.ToLower();
            //if (dictionary.Contains(value))
            //{
            //    method = value;
            //}
            //else throw new Exception($"Справочник не содержит метод - {value}");
        }

        public static void Execute(Package package)
        {
            _ = package ?? throw new ArgumentNullException("");

            if (dictionary.ContainsKey(package.Method))
            {
                Action execMethod = dictionary[package.Method];
                execMethod();
            }
        }


        public static void Get()
        {
            Console.WriteLine("");
        }
        public static void Post()
        {
            Console.WriteLine("");
        }
    }
}
