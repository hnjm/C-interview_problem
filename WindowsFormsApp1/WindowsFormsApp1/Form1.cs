using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Npgsql;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public string aesEncrypt(string sourceStr, string cryptKey, string cryptIV)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();              // MD5      Hash計算
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();     // SHA256   Hash計算

            byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(cryptKey));             // 計算AES的key
            byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(cryptIV));               // 計算AES的IV
            byte[] dataByteArray = Encoding.UTF8.GetBytes(sourceStr);                   // 將輸入字串轉為byte
            aes.Key = key;
            aes.IV = iv;
            ICryptoTransform cryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV);    // encrypt
            byte[] outputData = cryptoTransform.TransformFinalBlock(dataByteArray, 0, dataByteArray.Length);    //將encrypt後的array存到output
            return BitConverter.ToString(outputData).Replace("-", "");  //轉成hex string return

        }
        public string aesDecrypt(string sourceHexStr, string cryptKey, string cryptIV)
        {

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();              // MD5      Hash計算
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();     // SHA256   Hash計算

            byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(cryptKey));             // 計算AES的key
            byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(cryptIV));               // 計算AES的IV

            byte[] dataByteArray = Enumerable.Range(0, sourceHexStr.Length)
                                        .Where(x => x % 2 == 0)
                                        .Select(x => Convert.ToByte(sourceHexStr.Substring(x, 2), 16))
                                        .ToArray();                // 將輸入的Hex字串轉為byte
            aes.Key = key;
            aes.IV = iv;
            ICryptoTransform cryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV);    // decrypt
            byte[] outputData = cryptoTransform.TransformFinalBlock(dataByteArray, 0, dataByteArray.Length);    //將decrypt後的array存到output
            return Encoding.UTF8.GetString(outputData);  //轉成UTF的string return

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string encrypt = aesEncrypt("abc123", "aaaaaaaaaab", "aab");
            string decrypt = aesDecrypt(encrypt, "aaaaaaaaaab", "aab");
            textBox1.AppendText(encrypt + "\r\n");
            textBox1.AppendText(decrypt + "\r\n");

            /*string base64Decoded = aesEncrypt("abc123", "aaaaaaaaaab");
            string base64Encoded;
            byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(base64Decoded);
            base64Encoded = System.Convert.ToBase64String(data);

            textBox1.AppendText(base64Encoded + "\r\n");*/

            /*PostgreSQL postgreSQL = new PostgreSQL();
            List<string> rst = postgreSQL.PostgreSQLtest();
            foreach (string s in rst)
            {
                Console.WriteLine(s);
                textBox1.AppendText(s);
            }*/

        }
    }
}
