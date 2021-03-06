﻿using System;
using System.Linq;

namespace IP_calculator
{
    class IPcalc
    {
        #region Variables        
        internal byte[] ip_decimal = new byte[4];
        internal byte[] mask_decimal = new byte[4];
        internal byte[] invert_mask_decimal = new byte[4];
        internal string[] ip_binary = new string[4];
        internal string[] mask_binary = new string[4];
        internal string[] invert_mask_binary = new string[4];
        internal string[] nwAddress = new string[4];
        internal byte[] NetWorkAddress = new byte[4];
        internal string[] BroadcastAddr = new string[4];
        internal byte[] BroadCastAddress = new byte[4];
        internal int ShortMask;
        internal double Addresses;
        internal double Hosts;
        internal string MaskAndIP;
        internal string ErrorMessage;
        internal string Results;
        #endregion


        #region Control
        internal void StartWithDecimal(string[] ip, string[] mask)
        {
            if (Validation_Decimal(ip))
            {
                ip_decimal = itIsDecimal;
            }
            else
            {
                return;
            }

            if (Validation_Decimal(mask))
            {
                mask_decimal = itIsDecimal;
            }
            else
            {
                return;
            }
                        
            mask_binary = ConvertIPandMask_toBinary(mask_decimal);
            CalcShortMask(mask_binary);

            if (Validation_mask(mask_binary))
            {
                return;
            }

            ip_binary = ConvertIPandMask_toBinary(ip_decimal);
            MaskAndIP = $"\nIP: {MaskOrIP_ToString(ip_binary)} \n\nMask: {MaskOrIP_ToString(mask_binary)}";
            CalculateIp();
        }

        internal void StartWithBinary(string[] ip, string[] mask)
        {
            if (Validation_Binary(ip))
            {
                ip_binary = itIsBinary;
            }
            else
            {
                return;
            }

            if (Validation_Binary(mask))
            {
                mask_binary = itIsBinary;
            }
            else
            {
                return;
            }
            
            CalcShortMask(mask_binary);

            if (Validation_mask(mask_binary))
            {
                return;
            }

            ip_decimal = ConvertIPandMask_toDecimal(ip_binary);
            mask_decimal = ConvertIPandMask_toDecimal(mask_binary);

            MaskAndIP = $"IP: {MaskOrIP_ToString(ip_decimal)} \n\nMask: {MaskOrIP_ToString(mask_decimal)}";
            CalculateIp();
        }

        internal void CalculateIp()
        {
            CalcAddresses();
            CalcHosts();
            CalcNetWorkAddress(ip_binary, mask_binary);
            CalcIvertMask(mask_binary);
            CalcBroadcastAddress(nwAddress, invert_mask_binary);
            ResultOfCalculate();
        }

        internal void ResultOfCalculate()
        {
            Results = $"{MaskAndIP}"
                        + $"\n\n Short Mask: {ShortMask}"
                        + $"\n\n Inverted Mask: {MaskOrIP_ToString(invert_mask_decimal)}  |  {MaskOrIP_ToString(invert_mask_binary)}"
                        + $"\n\n Network: {MaskOrIP_ToString(NetWorkAddress)}  |  {MaskOrIP_ToString(nwAddress)}"
                        + $"\n\n Broadcast {MaskOrIP_ToString(BroadCastAddress)}  |  {MaskOrIP_ToString(BroadcastAddr)}"
                        + $"\n\n Addresses: {Addresses}"
                        + $"\n\n Hosts: {Hosts}";
        }
        #endregion


        #region Validation   
        private string[] itIsBinary;
        private bool Validation_Binary(string[] mustBeBinary)
        {
            itIsBinary = mustBeBinary;
            if (string.Concat(itIsBinary)
                      .Where(chr => chr != '1')
                      .Where(chr => chr != '0')
                      .Count() > 0)
            {
                ErrorMessage += "\nnumber should be 0 or 1";
                return false;
            }
            return true;
        }

        private byte[] itIsDecimal;
        private bool Validation_Decimal(string[] mustBeDecimal)
        {
            itIsDecimal = new byte[4];
            for (int i = 0; i < mustBeDecimal.Length; i++)
            {
                if (!byte.TryParse(mustBeDecimal[i], out itIsDecimal[i]))
                {
                    ErrorMessage += "\nnumber should be 0 - 255";
                    return false;
                }
            }
            return true;
        }

        private bool Validation_mask(string[] maskOfNetWork)
        {
            var last1 = string.Concat(maskOfNetWork).LastIndexOf('1');
            if (!(last1 < ShortMask))
            {
                ErrorMessage += "\nMask should contain 1, and then 0. Example 1111 1111.1111 1111.1111 1100.0000 0000";
                return true;
            }
            return false;
        }
        #endregion


        #region Converters        
        private string[] ConvertIPandMask_toBinary(byte[] maskORip)
        {
            var result = new string[4];
            for (int i = 0; i < ip_decimal.Length; i++)
            {
                result[i] = ConverterToBinary(maskORip[i]);
            }
            return result;
        }

        private string ConverterToBinary(byte value)
        {
            string result = Convert.ToString(value, 2);
            if (int.TryParse(result, out int _))
            {
                if (result.Length < 8)
                {
                    result = AddZeroTo8Numbers(result);
                }
                return result;
            }
            else
            {
                ErrorMessage += "\nError in converting decimal to binary";
                throw new ArgumentException("Error in converting decimal to binary");
            }
        }

        private string AddZeroTo8Numbers(string number)
        {
            do
            {
                number = "0" + $"{number}";
            }
            while (number.Length < 8);
            return number;
        }


        private byte[] ConvertIPandMask_toDecimal(string[] maskORip)
        {
            var result = new byte[4];
            for (int i = 0; i < maskORip.Length; i++)
            {
                result[i] = ConverterToDecimal(maskORip[i]);
            }
            return result;
        }

        private byte ConverterToDecimal(string value)
        {
            if (byte.TryParse(Convert.ToInt32(value, 2).ToString(), out byte result))
            {
                return result;
            }
            else
            {
                ErrorMessage += "Error in converting binary to decimal";
                throw new ArgumentException("Error in converting binary to decimal");
            }
        }

        private string MaskOrIP_ToString(string[] needConvertThis)
        {
            var res = "";
            foreach (var item in needConvertThis)
            {
                res += $"{item}.";
            }
            res = res.Remove(res.Length - 1);
            return res;
        }

        private string MaskOrIP_ToString(byte[] needConvertThis)
        {
            var res = "";
            foreach (var item in needConvertThis)
            {
                res += $"{item}.";
            }
            res = res.Remove(res.Length - 1);
            return res;
        }
        #endregion


        #region CalcOperators       

        private void CalcShortMask(string[] maskOfNetWork)
        {
            ShortMask = string.Concat(maskOfNetWork)
                              .Where(c => c == '1')
                              .Count();
        }

        private void CalcAddresses()
        {
            Addresses = Math.Pow(2, 32 - ShortMask);
        }

        private void CalcHosts()
        {
            if (Addresses < 3)
            {
                if (Addresses == 1)
                {
                    Hosts = 1;
                }
                else if (Addresses == 2)
                {
                    Hosts = 2;
                }
            }
            Hosts = Addresses - 2;
        }

        private void CalcIvertMask(string[] maskOfNetWork)
        {
            Array.Reverse(maskOfNetWork);
            for (int i = 0; i < maskOfNetWork.Length; i++)
            {
                invert_mask_binary[i] = ReverseString(maskOfNetWork[i]);
            }
            Array.Reverse(invert_mask_binary);
            invert_mask_decimal = ConvertIPandMask_toDecimal(invert_mask_binary);
        }

        public static string ReverseString(string s)
        {
            string res = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '1')
                {
                    res += '0';
                }
                else if (s[i] == '0')
                {
                    res += '1';
                }
            }
            return res;
        }

        private void CalcNetWorkAddress(string[] ip, string[] mask)
        {
            for (int i = 0; i < ip.Length; i++)
            {
                nwAddress[i] = AND_operation(ip[i], mask[i]);
            }
            NetWorkAddress = ConvertIPandMask_toDecimal(nwAddress);
        }

        private string AND_operation(string operator1, string operator2)
        {
            if (operator1.Length != operator2.Length)
            {
                throw new ArgumentException("operator1 != operator2");
            }
            char[] result = new char[operator1.Length];
            for (int i = 0; i < operator1.Length; i++)
            {
                if (operator1[i] == operator2[i])
                {
                    result[i] = operator1[i];
                }
                else
                {
                    result[i] = '0';
                }
            }
            return string.Concat(result);
        }

        private void CalcBroadcastAddress(string[] netAddress, string[] invMask)
        {
            for (int i = 0; i < netAddress.Length; i++)
            {
                BroadcastAddr[i] = OR_operation(netAddress[i], invMask[i]);
            }
            BroadCastAddress = ConvertIPandMask_toDecimal(BroadcastAddr);
        }

        private string OR_operation(string operator1, string operator2)
        {
            if (operator1.Length != operator2.Length)
            {
                throw new ArgumentException("operator1 != operator2");
            }
            string result = "";
            for (int i = 0; i < operator1.Length; i++)
            {
                if (operator1[i] == '1' || operator2[i] == '1')
                {
                    result += "1";
                }
                else
                {
                    result += "0";
                }
            }
            return result;
        }
        #endregion

    }
}
