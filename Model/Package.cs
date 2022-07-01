using Newtonsoft.Json;

namespace Model
{    
    public sealed class Package
    {
        public string Method { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public int Bandwidth { get => 64000; }
        public byte[] Data { get; set; }

        public string GetJson()
        {
            if (Name == null || Method == null)
            {
                throw new Exception("Файл отсутствует");
            }
            return JsonConvert.SerializeObject(this);
        }
    }
}