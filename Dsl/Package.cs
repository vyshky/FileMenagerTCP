using Newtonsoft.Json;

namespace Dsl
{
    public sealed class Package
    {
        private HashSet<string> dictionary = new HashSet<string>
        {
            "get",
            "post",
        };
        private string method = "get";
        public string Method
        {
            get => method;
            set
            {
                if (dictionary.Contains(value))
                {
                    method = value;
                }
            }
        }
        public string Name { get; set; }
        public int Length { get => 64000; }
        public byte[] Data { get; set; }

        public string GetJson()
        {
            if (Name == null || Data.Length == 0 || Method == string.Empty)
            {
                throw new Exception("Файл отсутствует");
            }
            return JsonConvert.SerializeObject(this);
        }
    }
}