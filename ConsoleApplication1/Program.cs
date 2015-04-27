using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    /**
     * FinBankAccNum-class stores the bank account number in computer format
     * 
     * Class takes the bank account number in following format: "123456-785" and
     * stores it in the computer format: "12345600000785"
     * 
     * @author Sauli Rajala
     * 
     */
    class FinBankAccNum
    {
        private long longFormat;
        private StringBuilder sb;
        private string shortFormat;

        /*
         * Constructor
         */
        public FinBankAccNum(String number) 
        {
            this.shortFormat = number;
            this.sb = new StringBuilder(number);
            
            this.sb.Remove(6,1);
                
		    if (this.sb.ToString().Substring(0, 1).Equals("4") || this.sb.ToString().Substring(0, 1).Equals("5"))
		    	addZeros(this.sb, 7);
		    else
			    addZeros(this.sb, 6);


            try
            {
                if (!validate(sb))
                {
                    Console.WriteLine("Tilinumeron " + number
                            + " tarkistenumero ei täsmää");
                    return;
                }
                this.longFormat = Int64.Parse(this.sb.ToString());
                
                
            }
            catch (FormatException)
            {
                Console.WriteLine("Syöttämäsi tilinumero " + number
                        + " ei ollut oikeaa muotoa");
            }
            catch (OverflowException)
            {
                Console.WriteLine("'{0}' on liian suuri.", number);
            }
	    }

        /*
         * This method validates the computer format of bank account number. It
         * checks if the checksum of account number is same as the last number in
         * bank account number.
         * 
         * Modified from the Stack Overflow -example:
         * http://stackoverflow.com/questions/20725761/validate-credit-card-number-using-luhn-algorithm
         * 
         * 
         * @param StringBuilder num - original bank account number
         * 
         * @return false - checksum is not the same as the last number
         * 
         * @return true - checksum is the same as the last number
         */
        private Boolean validate(StringBuilder num)
        {
            int tarkiste = Int16.Parse(num.ToString().Substring(13,1));
            char[] numChars = num.ToString().ToCharArray();
            int[] numInts = new int[13];
            for (int i = 0; i < 13; i++)
            {
                if (i % 2 == 0)
                {
                    numInts[i] = Int16.Parse(numChars[i].ToString()) * 2;
                    if (numInts[i] > 9)
                        numInts[i] = 1 + numInts[i] % 10;
                }
                else
                    numInts[i] = Int16.Parse(numChars[i].ToString());
            }

            int sum = 0;
            for (int i = 0; i < 13; i++)
            {
                sum += numInts[i];
            }

            int erotus = ((sum / 10 + 1) * 10) - sum;
            if (erotus % 10 == tarkiste)
                return true;
            return false;
        }

        /*
         * This method adds zeros to the bank account number
         * 
         * @param StringBuilder number - original bank account number
         * 
         * @param int startIndex - first index where to add zero
         */
        private void addZeros(StringBuilder number, int startIndex)
        {
            int i = 14 - number.Length;
            while (i > 0)
            {
                this.sb.Insert(startIndex, "0");
                i--;
            }
        }

        /*
         * get-method
         * 
         * @return longFormat - bank account number in computer format
         */
        public long getLongFormat()
        {
            return this.longFormat;
        }

        /*
         * get-method
         * 
         * @return shortFormat - bank account number in computer format
         */
        public string getShortFormat()
        {
            return this.shortFormat;
        }
    }

    /**
     * This is the "client-end" to test that FinBankAccNum-class is working properly
     * @author Sauli Rajala
     *
     */
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Moi");
            FinBankAccNum sourceAcNumNor = new FinBankAccNum("123456-785");
            FinBankAccNum sourceAcNumSP = new FinBankAccNum("423456-781");
            FinBankAccNum sourceAcNumOP = new FinBankAccNum("533802-596007");

            Console.WriteLine("Pankkitilinumero Nor = {0}, Long format = {1}", sourceAcNumNor.getShortFormat(), sourceAcNumNor.getLongFormat());
            Console.WriteLine("Pankkitilinumero SP = {0}, Long format = {1}", sourceAcNumSP.getShortFormat(), sourceAcNumSP.getLongFormat());
            Console.WriteLine("Pankkitilinumero OP = {0}, Long format = {1}", sourceAcNumOP.getShortFormat(), sourceAcNumOP.getLongFormat());

            //this isn't real bank account number because of the letter
		    FinBankAccNum sourceAcNumFAIL = new FinBankAccNum("510335-1A5999");
            Console.WriteLine("Pankkitilinumero SHB = {0}, Long format = {1}", sourceAcNumFAIL.getShortFormat(), sourceAcNumFAIL.getLongFormat());
		    
		    //this isn't real bank account number because the checksum doesn't match
		    //but if you change last number to 0, the checksum match
		    FinBankAccNum sourceAcNumSHB = new FinBankAccNum("110335-1535");
            Console.WriteLine("Pankkitilinumero SHB = {0}, Long format = {1}", sourceAcNumSHB.getShortFormat(), sourceAcNumSHB.getLongFormat());
            

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            
        }
    }
}
