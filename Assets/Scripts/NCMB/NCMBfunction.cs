namespace NCMB
{
    public class NCMBfunction
    {
        public static string model;
        public static string HandRecord;
        public static void Delete(string filename)
        {
            NCMBFile file = new NCMBFile(filename);
            file.DeleteAsync((NCMBException error) =>
            {
                if (error != null)
                {
                    // ���s
                }
                else
                {
                    // ����
                    UnityEngine.Debug.Log(" Delete Success");
                }
            });
        }
        public static void Read(string filename, out string data)
        {
            var content = "";
            NCMBFile file = new NCMBFile(filename);
            file.FetchAsync((byte[] fileData, NCMBException error) =>
            {
                if (error != null)
                {
                    // ���s
                    Read(filename,out content);
                }
                else
                {
                    // ����
                    content = System.Text.Encoding.UTF8.GetString(fileData);
                    UnityEngine.Debug.Log("Read Success");
                }
            });
            UnityEngine.Debug.Log(content.ToString());
            data = content;
        }
        public static void Save(string filename, string content)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
            NCMBFile file2 = new NCMBFile(filename, data);
            file2.SaveAsync((NCMBException error) =>
            {
                if (error != null)
                {
                    // ���s
                    Save(filename, content);
                }
                else
                {
                    // ����
                    UnityEngine.Debug.Log("Input Success");
                }
            });
        }
        public static void OverWrite(string filename, string content)
        {
            Delete(filename);
            Save(filename, content);
        }
    }
}