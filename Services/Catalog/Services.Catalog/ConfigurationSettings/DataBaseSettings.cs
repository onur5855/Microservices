namespace Services.Catalog.ConfigurationSettings
{
    public class DataBaseSettings : IDataBaseSettings
    {
        public string CourseCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }
    }
}
