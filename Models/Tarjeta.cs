using System;
using System.ComponentModel.DataAnnotations;

namespace CreditoWeb.Models
{
    public class Tarjeta
    {
        [Required(ErrorMessage = "El número de tarjeta es necesario.")]
        //[CreditCard]
        public string TarjetaNum { get; set; }
        public TipoTarjeta TipoTarjeta { get; set; }

        public bool Valida { get; set; }
     
        public Tarjeta(string tarjetaNum)
        {
            this.TarjetaNum = tarjetaNum;
            Valida = esValida();
            TipoTarjeta = tipoDeTarjeta();            
        }

        /// Basado en el algoritmo de Luhn determinar si esta tarjeta es valida
        /// como estamos dentro de la clase de tarjeta tenemos acceso a la propiedad TarjetaNum 
        private bool esValida()
        {
           if(TarjetaNum.Length>=14&&TarjetaNum.Length<=17){
            string numeroF1, numeroF2;
            int numEntF1, numEntF2;
            int reMOD, reNOMO = 0;
            int sumf1 = 0, sumFNL = 0;
     
            for(int i=TarjetaNum.Length-2; i==0; i-=2){
                numeroF1= TarjetaNum.Substring(i,1);
                numEntF1 = Convert.ToInt32(numeroF1);
                reMOD = numEntF1*2;
                if(reMOD>9){
                    reMOD = reMOD-9;
                }
                sumf1 = reMOD + sumf1; 
                }
                
            for(int c=TarjetaNum.Length-1; c==0; c-=2){
                numeroF2= TarjetaNum.Substring(c,1);
                numEntF2 = Convert.ToInt32(numeroF2);
              reNOMO = numEntF2 + reNOMO;
            }
          
          sumFNL = reNOMO + sumf1;
        Valida = (sumFNL%10==0);
             
          
        
        
        

        }
        return Valida;
        }

        /// Si la tarjeta es valida determinar de cuál tipo es VISA, MASTERCARD, AMERICANEXPRESS
        /// como estamos dentro de la clase de tarjeta tenemos acceso a la propiedad TarjetaNum 
        private TipoTarjeta tipoDeTarjeta()
        {
               if(TarjetaNum.Substring(0,1)=="4"){
                return TipoTarjeta.VISA;
            }
            if(TarjetaNum.Substring(0,2)=="51"||TarjetaNum.Substring(0,2)=="52"||TarjetaNum.Substring(0,2)=="53"||TarjetaNum.Substring(0,2)=="54"||TarjetaNum.Substring(0,2)=="55"){
                return TipoTarjeta.MASTERCARD;
            }
            if(TarjetaNum.Substring(0,2)=="34"||TarjetaNum.Substring(0,2)=="37"){
                return TipoTarjeta.AMERICANEXPRESS;
            }
           
            return TipoTarjeta.NOVALIDA;

        }


    }

    public enum TipoTarjeta
    {
        VISA,
        MASTERCARD,
        AMERICANEXPRESS,
        NOVALIDA

    }
}
