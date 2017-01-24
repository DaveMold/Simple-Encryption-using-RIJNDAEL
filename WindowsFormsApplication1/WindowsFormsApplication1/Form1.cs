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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //Public Vairbles
        byte encryptionTranslation = 4; //The amount the Ascii value is shifted by in the encrytion and decryption methods.
        int keySize = 128; //This stores the size of key required for the AES algorithims. The default values is the lowest bite value the key size can be set to.
        string defaultInputTxt = "Please Enter the text you would like to encrypt..."; //The text that should be displayed to tell the user what they must do when starting the program.
        string defaultKeyTxt = "16 characters"; //The text that should be displayed to tell the user the lenght of the key required for AES.
        byte[] chipherBytes; //This will be used to store the data we have encrypted.
        byte[] plainbytes; //This will be used to store the plain text data. eg readable data.
        enum Encrption_type { AES, RIJNDAEL }; //This enum will be used to toggle the encryption type that should be used by the system to encrypt your data.
        Encrption_type encrypt_typ = Encrption_type.RIJNDAEL; //inisalises the vairble that will store which of the encryption types should be used.
        SymmetricAlgorithm desObj;

        public Form1()
        {
            InitializeComponent();
            desObj = Rijndael.Create();
            desObj.KeySize = keySize; //Sets the key size that the AES algorithims will expect when encrypting or decrypting data.
            desObj.Mode = CipherMode.CBC; /*This type of encryption will insure that even if the text contains to of the same letters 
            beside each other it will still be encrypted as to values in the end to prevent a hit to the data contained for a malious attack. */
            desObj.Padding = PaddingMode.Zeros; //This will pad the data with "0" until the data can be stored as multiples of 16.
            //Sets up the event handler for the keyboard input on the input text box.
            this.Controls.Add(rtb_input);
            rtb_input.KeyPress += new KeyPressEventHandler(richTextBox1_KeyDown);
            rtb_input.Text = defaultInputTxt; //Sets the default value for the input text box on launch.
            txtB_key.Text = defaultKeyTxt; //Sets the default value for the key text box on launch.
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Input text box
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //Input text box
        public void richTextBox1_KeyDown(object sender, KeyPressEventArgs e)
        {
            //If when the user starts typeing the text box holds its default string then the string should be cleared before the users input is handled.
            if (rtb_input.Text == defaultInputTxt)
            {
                rtb_input.Clear();
                return;
            }
            ////If the users focus is on the input text box then check if the user has pressed the return key
            //if (e.KeyChar == (char)Keys.Enter && rtb_input.Focused == true)
            //{
            //    //If the return key is press the output text box should be cleared so that the result of the encryption can be stored instead.
            //    rtb_output.Clear();
            //    switch (encrypt_typ)
            //    {
            //        case Encrption_type.AES:
            //            rtb_output.Text += performAesEncryption(rtb_input.Text, txtB_key.Text);
            //            break;
            //        case Encrption_type.Basic:
            //            rtb_output.Text += performBasicEncryption(rtb_input.Text);
            //            break;
            //    }
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Output text box
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //Performs a encription of the string that is passed to the method and returns the result as a string.
        //Also checks if the string passed to the method is empty, if the string is empty then a error message box is displayed to prompt the user for a valid string.
        private string performBasicEncryption(string s)
        {
            //If the value to be ecnrypted is empty dont try and encrypt it show an error message.
            if (s == "" || s == null)
            {
                MessageBox.Show("Please enter a valid entry!");
                return "";
            }
            byte[] b = Encoding.ASCII.GetBytes(s); //Converts the string to an array of bytes that represent the chars Ascii value.
            s = ""; //Clears the string before populating it with the result of the encyption.
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i] == 32)
                    s += " ";
                //Every second element in the array of bytes will have the encryption amount added to the ascii value appose to having the amount deducted on every other element. 
                else if ((i % 2) == 0)
                    s += Convert.ToChar(b[i] + encryptionTranslation); //Deducts the encryption translation amount from the ascii value stored in the byte array and stores it in the result string.
                else
                    s += Convert.ToChar(b[i] - encryptionTranslation); //Adds the encryption translation amount to the ascii value stored in the byte array and stores it in the result string.
            }
            return s;
        }

        /*Performing AES encryption of a sting using a key also provided to the metode.
         * The first step is to check the key and insure that the value provided is 
         * not less than 16 charters in lenght. As the text box the key is taken from
         * will not allow the user to input more than 16 charters there is no need to 
         * check if the user has entered more than 16 charters.
         * The next step will be for the string of plain text and key to be convered to ascii.
         * Next the memory stream that will store our result is inisalised for use as well
         * as the crypto stream that will perform the encryption using the symmetrical algorthm
         * object that has been created with the type of encryption as well as the key
         * and padding type. After this it is required for this system that the result
         * be converted to Hex before it can be output in a text box for the user. If
         * the data is not converted to Hex then the data will be compramised when it
         * is output in the text box because not all ascii values when convered back
         *  to a string can be displayed in a text box. This will corupt the data and 
         * result in errors when trying to decrypt the corupted data.*/
        private string performRIJNDAELEncryption(string s, string k)
        {
            if (k.Length < keySize/8)
            {
                MessageBox.Show("Please insure your Key is " + keySize/8 + " charters long");
                return "";
            }
            //Stores the text to be encrypted as well as the key provided as Ascii values.
            plainbytes = Encoding.ASCII.GetBytes(s);
            desObj.Key = Encoding.ASCII.GetBytes(k);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desObj.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(plainbytes, 0, plainbytes.Length);
            cs.Close();
            chipherBytes = ms.ToArray();
            ms.Close();

            s = ByteArrayToString(chipherBytes);
            return s;
        }

        //Performs a decryption of the string that is passed to the method and returns the result as a string.
        //Also checks if the string passed to the method is empty, if the string is empty then a error message box is displayed to prompt the user for a valid string.
        private string performBasicDecryption(string s)
        {
            //If the value to be decrypted is empty dont try and decrypt it show an error message.
            if (s == "" || s == null)
            {
                MessageBox.Show("Please enter a valid entry!");
                return "";
            }
            Byte[] b = new byte[s.Length]; //Declairs an array of bytes to use to store the encrypted string at the lenght of the string in question.
            for (int i = 0; i < s.Length; i++)
            {
                b[i] = Convert.ToByte(s[i]); //Converts each element of the string to store it as an element in the byte array.
            }
            s = ""; //Clears the string before populating it with the relust of the decryption.
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i] == 32)
                    s += " ";
                //Every second element in the array of bytes will have the encryption amount deducted to the ascii value appose to having the amount added on every other element. 
                else if ((i % 2) == 0)
                    s += Convert.ToChar(b[i] - encryptionTranslation); //Deducts the encryption translation amount from the ascii value stored in the byte array and stores it in the result string.
                else
                    s += Convert.ToChar(b[i] + encryptionTranslation); //Deducts the encryption translation amount from the ascii value stored in the byte array and stores it in the result string.
            }
            return s;
        }

        /*Performs AES decryption to an encrypted string using the key that is passed to the method.
         * It will first check that the key the user has passed meets the requirements of the algorithim,
         * using the AES encryption will require the use of a key that is 16 charters in lenght. 
         * If the user enters a key that is less than 16 charters in lenght they will given an error message
         * shown as a message box requesting that they increase the key size. The metode will also return
         * an empty string and not perform the decription.
         * Due to an issue with text boxes displaying ascii values that cannot be displayed that would simply
         * show as "?", this would effectavly corrupt the encrypted string. To avoide this I convert the encrypted
         * data to HEX and out put it as a string. This means that during the decryption of the data I must convert
         * the data back to ascii in order to perform the decryption. To pervent this from happening as well as
         * using a Hex value I also use a try catch to catch the exception throuwn and create a message box that
         * will give information on the error instead of crashing the program.*/
        private string performRIJNDAELDecryption(string s, string k)
        {
            if (k.Length < keySize/8)
            {
                MessageBox.Show("Please insure your Key is " + keySize/8 + " charters long");
                return "";
            }
            //Stores the text to be decrypted as well as the key provided as Ascii values.
            try
            {
                chipherBytes = StringToByteArray(s);
                desObj.Key = Encoding.ASCII.GetBytes(k);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(chipherBytes);
                using (CryptoStream cs = new CryptoStream(ms, desObj.CreateDecryptor(), CryptoStreamMode.Read))

                {

                    cs.Read(chipherBytes, 0, chipherBytes.Length);

                    plainbytes = ms.ToArray();
                    cs.Close();
                    ms.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ln:181" + e.ToString());
            }

            s = Encoding.ASCII.GetString(plainbytes);
            return s;
        }

        //Encryption Button
        private void button1_Click(object sender, EventArgs e)
        {
            rtb_output.Clear(); //Clears the output text box before populating it with a new string of the encrypted text.
            switch (encrypt_typ)
            {
                case Encrption_type.AES:
                    rtb_output.Text += performRIJNDAELEncryption(rtb_input.Text, txtB_key.Text);
                    break;
                case Encrption_type.RIJNDAEL:
                    rtb_output.Text += performBasicEncryption(rtb_input.Text);
                    break;
            }
        }

        //Decryption Button
        private void button2_Click(object sender, EventArgs e)
        {
            rtb_output.Clear(); //Clears the output text box before populating it with a new string of the decrypted text.
            switch (encrypt_typ)
            {
                case Encrption_type.AES:
                    rtb_output.Text += performRIJNDAELDecryption(rtb_input.Text, txtB_key.Text);
                    break;
                case Encrption_type.RIJNDAEL:
                    rtb_output.Text += performBasicDecryption(rtb_input.Text);
                    break;
            }
        }

        //Clears the input and key text boxs of any data.
        private void bt_resetTxtb_Click(object sender, EventArgs e)
        {
            rtb_input.Clear(); //Clears the input text box of any text when the reset butten is clicked.
            if (encrypt_typ == Encrption_type.AES)
                txtB_key.Clear(); //Clears the key text box of any text when the reset butten is clicked.
        }

        private void txtB_key_TextChanged(object sender, EventArgs e)
        {
        }

        private void radbtn_basicEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            txtB_key.ReadOnly = true;
            encrypt_typ = Encrption_type.RIJNDAEL;
        }

        private void radbtn_AesEncryt_CheckedChanged(object sender, EventArgs e)
        {
            txtB_key.ReadOnly = false;
            encrypt_typ = Encrption_type.AES;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //Convers an array of bytes to a string of Hex values.
        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        //Convers a string of Hex values to an array of Bytes.
        private static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        /*If the data in the number up down text box is changed then check if the key size == the value it stores
        * if the two values differ then set the number up down box value to the new key size.*/
        private void numUpDown_KeySize_ValueChanged(object sender, EventArgs e)
        {
            if (numUpDown_KeySize.Value * 8 != keySize)
                setKeySize(numUpDown_KeySize.Value);
        }

        /*When passed a value for the amount of charters it will multiply the value by 8 in order to get the bit
        * value and assign it to the key size. */
        private void setKeySize(decimal n)
        {
            keySize = Convert.ToInt32(8 * n);
            txtB_key.MaxLength = Convert.ToInt32(n);
        }
    }
}
