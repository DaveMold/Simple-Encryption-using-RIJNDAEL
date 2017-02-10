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
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //Public Vairbles
        byte encryptionTranslation = 4; //The amount the Ascii value is shifted by in the encrytion and decryption methods.
        int keySizebit = 128; //This stores the size of key required for the AES algorithims. The default values is the lowest bite value the key size can be set to.
        string defaultInputTxtForTxt = "Please enter the text you would like to encrypt..."; //The text that should be displayed to tell the user what they must do when starting the program.
        string defaultInputTxtForFile = "Please enter the file path of the file you would like to encrypt..."; /*The text that should be displayed to tell the user what they must do when 
        * the file option is ticked.*/
        string defaultAESKeyTxt = " characters"; //The text that should be displayed to tell the user the lenght of the key required for AES.
        string defaultBasicKeyTxt = "No key is required."; //This is the text that will be displayed if there is no key required for the algorithim. eg Basic encryption.
        string defaultIVTxt = "Default IV"; //This is used to fill the IV text box that the user may opt to set there own IV.
        byte[] chipherBytes; //This will be used to store the data we have encrypted.
        byte[] plainbytes; //This will be used to store the plain text data. eg readable data.
        string path = ""; //Inisalises the path vairable for the encryption of a file.
        List<string> paths = new List<string>(); //Stores the path of the file the user wants to encrypt, if the user selects multiple files then each path will be stored as an element in the list.
        bool copyFile = true; //If the user has checked copy file then when encrypting the file a secound file will be created as a back up.
        enum Encrption_type { BASIC, RIJNDAEL }; //This enum will be used to toggle the encryption type that should be used by the system to encrypt your data.
        Encrption_type encrypt_typ = Encrption_type.BASIC; //inisalises the vairble that will store which of the encryption types should be used.
        enum Data_Type { TEXT, FILE }; //This will be used to toggle between encrypting files or raw text.
        Data_Type dataTypeForEncrypt = Data_Type.TEXT; //inisalised to text as on program launch the data type for encryption is set to text.
        SymmetricAlgorithm desObj;
        bool useDefaultIV = true; //Defines weater or not the user has chosen to use there own IV or to use a default value set by the application.
        byte[] defaultIV = new byte[] { 1, 8, 6, 3, 2, 5, 7, 8, 2, 4, 8, 9, 6, 2, 5, 9 }; //Default IV to be used if not defined by the system.

        public Form1()
        {
            InitializeComponent();
            InaliseDesObj();
            this.Controls.Add(rtb_input);
            this.Controls.Add(txtB_key);
            this.Controls.Add(txtbx_IV);
            rtb_input.KeyPress += new KeyPressEventHandler(richTextBox1_KeyDown);
            txtB_key.KeyPress += new KeyPressEventHandler(txtB_key_KeyDown);
            txtbx_IV.KeyPress += new KeyPressEventHandler(txtbx_IV_KeyDown);
            rtb_input.Text = defaultInputTxtForTxt; //Sets the default value for the input text box on launch.
            txtB_key.Text = defaultBasicKeyTxt; //Sets the default value for the key text box on launch.
            txtbx_IV.Text = defaultIVTxt; //Sets the default value for the IV text box on launch.
            numUpDown_KeySize.Enabled = false; /*As there is no need to set a key with the Basic encryption, the number up down txt box can be
            disabled until the user selects AES encryption.*/
        }

        //Sets up the type of encryption as well as the key peramiters and padding type for the encryption algoithim.
        private void InaliseDesObj()
        {
            desObj = Rijndael.Create();
            desObj.IV = defaultIV;
            desObj.KeySize = keySizebit; //Sets the key size that the AES algorithims will expect when encrypting or decrypting data.
            desObj.Mode = CipherMode.CBC; /*This type of encryption will insure that even if the text contains to of the same letters 
            beside each other it will still be encrypted as to values in the end to prevent a hit to the data contained for a malious attack. */
            desObj.Padding = PaddingMode.Zeros; //This will pad the data with "0" until the data can be stored as multiples of 16.
            //Sets up the event handler for the keyboard input on the input text box.
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
            if (rtb_input.Text == defaultInputTxtForTxt || rtb_input.Text == defaultInputTxtForFile)
            {
                rtb_input.Clear();
                return;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Output text box
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        /*Performs a encription of the string or file that is passed to the method and returns the result as a string or a message that the file has been encrypted.
        * Also checks if the string passed to the method is empty, if the string is empty then a error message box is displayed to prompt the user for a valid string.
        * As well as checking for a valid path for the file that is to be encrypted.*/
        private string performBasicEncryption(string s)
        {
            List<byte> b = new List<byte>(); /*Creates a byte list for use to store the data for encryption. The reason
            I require a list is that a vaible for use through out this method is needed but as I do not know the size of
            the data I will have is not clear at this point so a list will allow me to change the size of the array as I need it.*/

            //Switch used to check the type of data being encrypted, data can be a text string or a path to a file.
            switch (dataTypeForEncrypt)
            {
                case Data_Type.TEXT:
                    {
                        //If the value to be ecnrypted is empty dont try and encrypt it show an error message.
                        if (s == "" || s == null)
                        {
                            MessageBox.Show("Please enter a valid entry!");
                            return "";
                        }

                        byte[] temp;
                        temp = Encoding.ASCII.GetBytes(s); //Converts the string to an array of bytes that represent the chars Ascii value.
                        /*Used to tranfer the data in the temp array that stores the data from the GetBytes method to the list 
                         * I have for storing the data. */
                        foreach (byte element in temp)
                        {
                            b.Add(element);
                        }

                        s = ""; //Clears the string before populating it with the result of the encyption.
                    }
                    break;
                case Data_Type.FILE:
                    {
                        path = s;
                        if(copyFile)
                            copy_File(path); /*This Will create a copy of the file you wish to encrypt so that you do not loose your data if you 
                        * have had an issue with the encryption or have lost your key.*/
                        try
                        {
                            byte[] temp;
                            temp = File.ReadAllBytes(path); /*Reads the data as bytes to an array from the file of the path
                            provided.*/
                            /*Used to tranfer the data in the temp array that stores the data from the GetBytes method to the list 
                         * I have for storing the data. */
                            foreach (byte element in temp)
                            {
                                b.Add(element);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please enter a valid file path or insure the path is pointing to the correct file.\n" + ex.Message);
                            return "";
                        }
                    }
                    break;
            }

            //Switch used to check the type of data being encrypted, data can be a text string or a path to a file.
            switch (dataTypeForEncrypt)
            {
                case Data_Type.TEXT:
                    {
                        for (int i = 0; i < b.Count; i++)
                        {
                            //Leaves the spaces alone.
                            if (b[i] == 32)
                                s += " ";
                            //Every second element in the array of bytes will have the encryption amount added to the ascii value appose to having the amount deducted on every other element. 
                            else if ((i % 2) == 0)
                                s += Convert.ToChar(b[i] + encryptionTranslation); //Deducts the encryption translation amount from the ascii value stored in the byte array and stores it in the result string.
                            else
                                s += Convert.ToChar(b[i] - encryptionTranslation); //Adds the encryption translation amount to the ascii value stored in the byte array and stores it in the result string.
                        }
                    }
                    break;
                case Data_Type.FILE:
                    {
                        for (int i = 0; i < b.Count; i++)
                        {
                            //Every second element in the array of bytes will have the bytes offset on every other element. 
                            if ((i % 2) == 0)
                                b[i]++; //Offsetting the bytes read from the file.
                            else
                                b[i]--; //Offsets the bytes read from the file.
                        }
                        //write to file.
                        //As the writting to the file requires the data in an array we must populate the array from the list of bytes.
                        byte[] temp = new byte[b.Count];
                        for (int i = 0; i < b.Count; i++)
                        {
                            temp[i] = b[i];
                        }
                        File.WriteAllBytes(path, temp);
                        s = getFileNameFromPath(path) + " File encrypted.\n"; //shows to the user that there file has been encrypted.
                    }
                    break;
            }
            return s;
        }

        /*Performing AES encryption of a sting or file at a path provided using a key also provided to the metode.
         * The first step is to check the key and insure that the value provided is 
         * not less than 16 charters in lenght. As the text box the key is taken from
         * will not allow the user to input more than 16 charters there is no need to 
         * check if the user has entered more than 16 charters.
         * The next step will be for the string of plain text or the the data read from the file
         * and key to be convered to ascii.
         * Next the memory stream that will store our result is inisalised for use as well
         * as the crypto stream that will perform the encryption using the symmetrical algorthm
         * object that has been created with the type of encryption as well as the key
         * and padding type. After this it is required for this system that the result
         * be converted to Hex before it can be output in a text box for the user. If
         * the data is not converted to Hex then the data will be compramised when it
         * is output in the text box because not all ascii values when convered back
         * to a string can be displayed in a text box. This will corupt the data and 
         * result in errors when trying to decrypt the corupted data.
         * If the file encryption is used the data will be writen back to the file
         * and a result message will be displaied to the user.*/
        private string performRIJNDAELEncryption(string s, string k, byte[] iv)
        {
            if (k.Length < keySizebit/8)
            {
                MessageBox.Show("Please insure your Key is " + keySizebit/8 + " charters long");
                return "";
            }

            if (iv.Length < 16)
            {
                MessageBox.Show("Please insure your IV is 16 charters long");
                return "";
            }
            else
                desObj.IV = iv;

            //Switch used to check the type of data being encrypted, data can be a text string or a path to a file.
            switch (dataTypeForEncrypt)
            {
                case Data_Type.TEXT:
                    plainbytes = Encoding.ASCII.GetBytes(s);
                    break;
                case Data_Type.FILE:
                    path = s;
                    if(copyFile)
                        copy_File(path);/*This Will create a copy of the file you wish to encrypt so that you do not loose your data if you 
                    * have had an issue with the encryption or have lost your key.*/
                    try
                    {
                        plainbytes = File.ReadAllBytes(path); /*Reads the data as bytes to an array from the file of the path
                        * provided.*/
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please enter a valid file path or insure the path is pointing to the correct file.\n" + ex.Message);
                        return "";
                    }
                    break;
            }

            //Stores the key provided as Ascii values.
            desObj.Key = Encoding.ASCII.GetBytes(k);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desObj.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(plainbytes, 0, plainbytes.Length);
            cs.Close();
            chipherBytes = ms.ToArray();
            ms.Close();

            //Switch used to check the type of data being encrypted, data can be a text string or a path to a file.
            switch (dataTypeForEncrypt)
            {
                case Data_Type.TEXT:
                    s = ByteArrayToString(chipherBytes); //Converts the bytes result to a string and returns it as the result.
                    break;
                case Data_Type.FILE:
                    File.WriteAllBytes(path, chipherBytes);
                    s = getFileNameFromPath(path) + " File encrypted.\n"; //shows to the user that there file has been encrypted.
                    break;
            }
           
            return s;
        }

        /*Performs a decryption of the string that is passed to the method and returns the result as a string.
        * Also checks if the string passed to the method is empty, if the string is empty then a error message box is displayed to prompt the user for a valid string.
        * As well as checking for a valid path for the file that is to be decrypted.*/
        private string performBasicDecryption(string s)
        {
            //If the value to be decrypted is empty dont try and decrypt it show an error message.
            if (s == "" || s == null)
            {
                MessageBox.Show("Please enter a valid entry!");
                return "";
            }

            List<byte> b = new List<byte>(); /*Creates a list to store the bytes from the data as at this point the size of the 
            * array needed is unclear, but the vairble will be required throughout the method.*/
            string path = "";

            //Switch used to check the type of data being decrypted, data can be a text string or a path to a file.
            switch (dataTypeForEncrypt)
            {
                case Data_Type.TEXT:
                    {
                        for (int i = 0; i < s.Length; i++)
                        {
                            b.Add(Convert.ToByte(s[i])); //Converts each element of the string to store it as an element in the byte array.
                        }
                        s = ""; //Clears the string before populating it with the relust of the decryption.
                    }
                    break;
                case Data_Type.FILE:
                    {
                        path = s; //The string of the path is stored for collection and populating to and from the file.
                        try
                        {
                            /*A temp vairble is required as ReadAllBytes method requrns an array, the temp array will 
                            * then need to be transfered to the list that sorts the data within the scope of this method. */
                            byte[] temp;
                            temp = File.ReadAllBytes(path);
                            foreach (byte element in temp)
                            {
                                b.Add(element);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please enter a valid file path.\n" + ex.Message);
                            return "";
                        }
                    }
                    break;
            }

            //Switch used to check the type of data being decrypted, data can be a text string or a path to a file.
            switch (dataTypeForEncrypt)
            {
                case Data_Type.TEXT:
                    {

                        for (int i = 0; i < b.Count; i++)
                        {
                            if (b[i] == 32)
                                s += " ";
                            //Every second element in the array of bytes will have the encryption amount deducted to the ascii value appose to having the amount added on every other element. 
                            else if ((i % 2) == 0)
                                s += Convert.ToChar(b[i] - encryptionTranslation); //Deducts the encryption translation amount from the ascii value stored in the byte array and stores it in the result string.
                            else
                                s += Convert.ToChar(b[i] + encryptionTranslation); //Deducts the encryption translation amount from the ascii value stored in the byte array and stores it in the result string.
                        }
                    }
                    break;
                case Data_Type.FILE:
                    {
                        for (int i = 0; i < b.Count; i++)
                        {
                            //Every second element in the array of bytes will have the bytes shifted to decrypted the data of every element. 
                            if ((i % 2) == 0)
                                b[i]--; //Offsets the bytes to decrypt the data.
                            else
                                b[i]++; //Offsets the bytes to decrypt the data.
                        }
                        //write to file.
                        /*Again the data must be stored in a temp array in order to be used by the WriteAllBytes method that expects 
                         * an array not a list.*/
                        byte[] temp = new byte[b.Count];
                        for (int i = 0; i < b.Count; i++)
                        {
                            temp[i] = b[i];
                        }
                        File.WriteAllBytes(path, temp);
                        s = getFileNameFromPath(path) + " File decrypted.\n";//A message is returned to the user of the success senario.
                    }
                    break;
            }
            return s;
        }

        /*Performs AES decryption to an encrypted string or file at a given path using the key that is 
         * passed to the method. It will first check that the key the user has passed meets the requirements of the algorithim,
         * using the AES encryption will require the use of a key that is 16 charters in lenght. 
         * If the user enters a key that is less than 16 charters in lenght they will given an error message
         * shown as a message box requesting that they increase the key size. The metode will also return
         * an empty string and not perform the decription.
         * Due to an issue with text boxes displaying ascii values that cannot be displayed that would simply
         * show as "?", this would effectavly corrupt the encrypted string. To avoide this I convert the encrypted
         * data to HEX and out put it as a string. This means that during the decryption of the data I must convert
         * the data back to ascii in order to perform the decryption. To pervent this from happening as well as
         * using a Hex value I also use a try catch to catch the exception throuwn and create a message box that
         * will give information on the error instead of crashing the program. If the user is decrypting a file
         * the result will be writen back to the file as well as a message to the user of the result.*/
        private string performRIJNDAELDecryption(string s, string k, byte[] iv)
        {
            if (k.Length < keySizebit/8)
            {
                MessageBox.Show("Please insure your Key is " + keySizebit/8 + " charters long");
                return "";
            }


            if (iv.Length < 16)
            {
                MessageBox.Show("Please insure your IV is 16 charters long");
                return "";
            }
            else
                desObj.IV = iv;
           

            //Switch used to check the type of data being decrypted, data can be a text string or a path to a file.
            switch (dataTypeForEncrypt)
            {
                case Data_Type.TEXT:
                    chipherBytes = StringToByteArray(s);
                    break;
                case Data_Type.FILE:
                    path = s;
                    try
                    {
                        chipherBytes = File.ReadAllBytes(path); /*Reads the data as bytes to an array from the file of the path
                        * provided.*/
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please enter a valid file path or insure the path is pointing to the correct file.\n" + ex.Message);
                        return "";
                    }
                    break;
            }

            try
            {
                //Stores the key provided as Ascii values.
                desObj.Key = Encoding.ASCII.GetBytes(k);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(chipherBytes); //creates a memory stream to store the result.
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

            //Switch used to check the type of data being decrypted, data can be a text string or a path to a file.
            switch (dataTypeForEncrypt)
            {
                case Data_Type.TEXT:
                    s = Encoding.ASCII.GetString(plainbytes); //Converts the bytes result to a string and returns it as the result.
                    break;
                case Data_Type.FILE:
                    File.WriteAllBytes(path, plainbytes);
                    s = getFileNameFromPath(path) + " File decrypted.\n"; //shows to the user that there file has been decrypted.
                    break;
            }

            return s;
        }

        //Encryption Button
        private void button1_Click(object sender, EventArgs e)
        {
            rtb_output.Clear(); //Clears the output text box before populating it with a new string of the encrypted text.
            switch (encrypt_typ)
            {
                case Encrption_type.RIJNDAEL:
                    /*If the user dose not wish to enter there own IV the default IV is used for the encryption. If the 
                    * user dose chose to enter there own IV it will be passed to the encryption method and checked
                    * for issues that could be presented.*/
                    if (useDefaultIV)
                    {
                        if (!openFileDialog1.Multiselect)
                            rtb_output.Text += performRIJNDAELEncryption(rtb_input.Text, txtB_key.Text, defaultIV);
                        else
                        {
                            rtb_output.Clear();
                            foreach (string p in paths)
                            {
                                rtb_output.Text += performRIJNDAELEncryption(p, txtB_key.Text, defaultIV);
                            }
                        }
                    }
                    else
                    {
                        /*This will cycle through the string from the IV feild convering each char to a byte and updating
                        * the byte array. The byte array is then passed to the encryption method. */
                        string t = txtbx_IV.Text;
                        byte[] temp = new byte[t.Length];
                        int i = 0;
                        foreach (char c in t)
                        {
                            temp[i] = (byte)c;
                            i++;
                        }
                        if (!openFileDialog1.Multiselect)
                            rtb_output.Text += performRIJNDAELEncryption(rtb_input.Text, txtB_key.Text, temp);
                        else
                        {
                            rtb_output.Clear();
                            foreach (string p in paths)
                            {
                                rtb_output.Text += performRIJNDAELEncryption(p, txtB_key.Text, temp);
                            }
                        }
                    }
                    break;
                case Encrption_type.BASIC:
                    if (!openFileDialog1.Multiselect)
                        rtb_output.Text += performBasicEncryption(rtb_input.Text);
                    else
                    {
                        rtb_output.Clear();
                        foreach (string p in paths)
                        {
                            rtb_output.Text += performBasicEncryption(p);
                        }
                    }
                    break;
            }
        }

        //Decryption Button
        private void button2_Click(object sender, EventArgs e)
        {
            rtb_output.Clear(); //Clears the output text box before populating it with a new string of the decrypted text.
            switch (encrypt_typ)
            {
                case Encrption_type.RIJNDAEL:
                    /*If the user dose not wish to enter there own IV the default IV is used for the decryption. If the 
                    * user dose chose to enter there own IV it will be passed to the decryption method and checked
                    * for issues that could be presented.*/
                    if (useDefaultIV)
                    {
                        if (!openFileDialog1.Multiselect)
                            rtb_output.Text = performRIJNDAELDecryption(rtb_input.Text, txtB_key.Text, defaultIV);
                        else
                        {
                            rtb_output.Clear();
                            foreach (string p in paths)
                            {
                                rtb_output.Text += performRIJNDAELDecryption(p, txtB_key.Text, defaultIV);
                            }
                        }
                    }
                    else
                    {
                        /*This will cycle through the string taken from the IV feild, each char will be convered to 
                         * a byte value and be stored in the temp byte array. Once the byte array is populated it is
                         * passed to the decryption method.*/
                        string t = txtbx_IV.Text;
                        byte[] temp = new byte[t.Length];
                        int i = 0;
                        foreach (char c in t)
                        {
                            temp[i] = (byte)c;
                            i++;
                        }
                        if (!openFileDialog1.Multiselect)
                            rtb_output.Text = performRIJNDAELDecryption(rtb_input.Text, txtB_key.Text, temp);
                        else
                        {
                            rtb_output.Clear();
                            foreach (string p in paths)
                            {
                                rtb_output.Text += performRIJNDAELDecryption(p, txtB_key.Text, temp);
                            }
                        }
                    }
                    break;
                case Encrption_type.BASIC:
                    if (!openFileDialog1.Multiselect)
                        rtb_output.Text = performBasicDecryption(rtb_input.Text);
                    else
                    {
                        rtb_output.Clear();
                        foreach (string p in paths)
                        {
                            rtb_output.Text += performBasicDecryption(p);
                        }
                    }
                    break;
            }
        }

        //Strips the path of the folder higharky leaving just the file name.
        private string getFileNameFromPath(string path)
        {
            int lastIndex = 0, index = 0;
            /*Loops through the path looking for the \ char counting each char as I pass through,
            * when the last of the \ chars are found the value is stored in the lastIndex vairble.
            * The lastIndex value is then used to strip the path of the folder details.*/
            foreach (char c in path)
            {
                index++;
                if (c == '\\')
                {
                    lastIndex = index;
                }
            }

            return path.Substring(lastIndex);
        }

        //Clears the input and key text boxs of any data.
        private void bt_resetTxtb_Click(object sender, EventArgs e)
        {
            rtb_input.Clear(); //Clears the input text box of any text when the reset butten is clicked.
            if (encrypt_typ == Encrption_type.RIJNDAEL)
            {
                txtB_key.Clear(); //Clears the key text box of any text when the clear text butten is clicked.
                if (!useDefaultIV)
                    txtbx_IV.Clear();
            }
        }

        private void txtB_key_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtB_key_KeyDown(object sender, KeyPressEventArgs e)
        {
            //If when the user starts typeing the text box holds its default string then the string should be cleared before the users input is handled.
            if (txtB_key.Text == defaultBasicKeyTxt || txtB_key.Text == (keySizebit / 8) + defaultAESKeyTxt)
            {
                txtB_key.Clear();
                return;
            }
        }

        private void radbtn_basicEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            /*With the Basic encryption there is no need for a key the number up down text box
             * therefore this can be disabled, as well as the key text box. The default key text
             * that explains the requirements of the key can be set to the keys text boxs text.*/
            numUpDown_KeySize.Enabled = false;
            txtB_key.ReadOnly = true;
            txtB_key.Enabled = false;
            txtB_key.Text = defaultBasicKeyTxt;
            encrypt_typ = Encrption_type.BASIC;
            chbx_UseIV.Enabled = false;
        }

        private void radbtn_AesEncryt_CheckedChanged(object sender, EventArgs e)
        {
            /*With the Rijndael encryption a key is required so the key text box should be
             * enabled as well as the number up down text box for selecting the key lenght.
             * The key text feild is also set the have a discriptive value for what the size
             * of the key required will be until the user enters his or her key.*/
            numUpDown_KeySize.Enabled = true;
            txtB_key.ReadOnly = false;
            txtB_key.Enabled = true;
            //In order to convert the key size from bits to an int value we must devied the value by 8.
            txtB_key.Text = (keySizebit/8) + defaultAESKeyTxt;
            encrypt_typ = Encrption_type.RIJNDAEL;
            chbx_UseIV.Enabled = true;
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
            if (encrypt_typ == Encrption_type.RIJNDAEL)
            {
                if (numUpDown_KeySize.Value * 8 != keySizebit)
                    setKeySize(numUpDown_KeySize.Value);
                txtB_key.Text = (keySizebit / 8) + defaultAESKeyTxt;
            }
        }

        /*When passed a value for the amount of charters it will multiply the value by 8 in order to get the bit
        * value and assign it to the key size. */
        private void setKeySize(decimal n)
        {
            keySizebit = Convert.ToInt32(8 * n);
            txtB_key.MaxLength = Convert.ToInt32(n);
        }

        private void radbn_txtData_CheckedChanged(object sender, EventArgs e)
        {
            chBx_MultiSelect.Enabled = false;
            bn_BrousePath.Enabled = false;
            chkbx_CopyFile.Enabled = false;
            rtb_input.Text = defaultInputTxtForTxt;
            dataTypeForEncrypt = Data_Type.TEXT;
        }

        private void radbn_fileData_CheckedChanged(object sender, EventArgs e)
        {
            chBx_MultiSelect.Enabled = true;
            bn_BrousePath.Enabled = true;
            chkbx_CopyFile.Enabled = true;
            rtb_input.Text = defaultInputTxtForFile;
            dataTypeForEncrypt = Data_Type.FILE;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (openFileDialog1.Multiselect)
            {
                rtb_input.Clear();
                paths.Capacity = openFileDialog1.FileNames.Length;
                foreach (string n in openFileDialog1.FileNames)
                {
                    rtb_input.Text += n + "\n";
                    paths.Add(n);
                }
            }
            else
            {
                rtb_input.Text = openFileDialog1.FileName;
            }
        }

        private void bn_BrousePath_Click(object sender, EventArgs e)
        {
            if (chBx_MultiSelect.Checked)
                openFileDialog1.Multiselect = true;
            else
                openFileDialog1.Multiselect = false;

            openFileDialog1.ShowDialog();
        }

        //This will Amend "Copy" to the end of the files name and then re-add the extention before creating a copy of a file.
        private void copy_File(string path)
        {
            char[] newPath = path.ToArray();
            string str_newPath = "";

            int size = path.Length;
            newPath[size - 4] = Convert.ToChar("C");
            newPath[size - 3] = Convert.ToChar("o");
            newPath[size - 2] = Convert.ToChar("p");
            newPath[size - 1] = Convert.ToChar("y");

            foreach (char c in newPath)
            {
                str_newPath += c;
            }

            for (int i = 0; i < path.Length; i++)
            {
                if (i >= path.Length - 4)
                    str_newPath += path[i];
            }
            try
            {
                System.IO.File.Copy(path, str_newPath);
                rtb_output.Text = "Copy of File made.\n";
            }
            catch (Exception e)
            {
                MessageBox.Show("Please insure you have not already got a file at the Path you provided named (YourFileName Copy . extention). " + e.Message);
            }
        }

        private void chkbx_CopyFile_CheckedChanged(object sender, EventArgs e)
        {
            switch (chkbx_CopyFile.CheckState)
            {
                case CheckState.Checked:
                    copyFile = true;
                    break;
                case CheckState.Unchecked:
                    copyFile = false;
                    break;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lb_DataType_Click(object sender, EventArgs e)
        {

        }

        private void txtbx_IV_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void txtbx_IV_KeyDown(object sender, KeyPressEventArgs e)
        {
            //If when the user starts typeing the text box holds its default string then the string should be cleared before the users input is handled.
            if (txtbx_IV.Text == defaultIVTxt)
            {
                txtbx_IV.Clear();
                return;
            }
        }

        private void chbx_UseIV_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_UseIV.Checked)
            {
                txtbx_IV.Text = defaultIVTxt;
                txtbx_IV.Enabled = false;
                useDefaultIV = true;
            }
            else
            {
                txtbx_IV.Enabled = true;
                useDefaultIV = false;
            }
        }

        private void encryt_type_Click(object sender, EventArgs e)
        {

        }
    }
}
