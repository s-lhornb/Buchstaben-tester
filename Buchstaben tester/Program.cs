using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Buchstaben_tester
{
    class Program
    {

        /*
            Nutzen Sie Hilfe (aus dem Internet usw.) bitte ausschließlich, um die LINQ-Methoden und die Technik von Lambdas zu verstehen.




            Schreiben Sie einen Ausdruck in C#, der feststellt, ob in einem String kein Buchstaben mehrfach vorkommt. 

            Ich habe es so ausgelegt, dass keine Sonderzeichen geprüft werden sollen und es für die Suche egal ist, ob es ein Klein- oder Großbuchstabe ist.






            Es gibt eine Lösung, die in einem kurzen Ausdruck mit drei LINQ-Methoden auskommt.

            Für die Lösung erstellen Sie bitte eine fachlich korrekte Erläuterung, wie und warum sie funktioniert.

            Zu Übungszwecken können Sie probieren, wie die Lösung ohne LINQ aussieht. Das sind dann sehr viel mehr Zeilen.
         */
        static void Main(string[] args)
        {
            string teststring = "";
            Action Testen = () =>
            {
                Console.WriteLine("Bitte geben sie einen Text zum Testen ein oder \"STOP\" zum Stoppen des Programms");
                teststring = Console.ReadLine();
                if (!teststring.Equals("STOP"))
                {
                    Console.WriteLine("Test 1 mit LINQ hat {0}doppelten Buchstaben gefunden.", (CharacterTesterLINQ1(teststring) ? "keine " : "mindestens einen "));
                    Console.WriteLine("Test 2 mit LINQ und Lambdas hat {0}doppelten Buchstaben gefunden.", (CharacterTesterLINQ2(teststring) ? "keine " : "mindestens einen "));
                    Console.WriteLine();
                }
            };

            while (!teststring.Equals("STOP"))
            {
                Testen();
                
            }
        }

        /// <summary>
        /// Benutzt nur LINQ, vergleichbar aufgebaut wie SQL-Code, um herauszufinden, ob doppelte Buchstaben in einem String sind.
        /// </summary>
        /// <param name="s">Zu testender String</param>
        /// <returns>Ergebnis, ob String keine doppelten Buchstaben hat: Ture = keine, False = mind. einen</returns>
        private static bool CharacterTesterLINQ1(string s)
        {
            return (from char chars in Regex.Replace(s.ToLower(), @"[^a-z]", "") //übernimmt den String, welcher hier in Kleinbuchstaben umgewandelt und dann von allen nicht Kleinbuchstaben gereinigt wird. 
                                                                                 //Das Entfernen der Sonderzeichen könnte auch mit einem Where-Filter durchgeführt werden, welcher vor der group-Methode durchgeführt würde.
                    group chars by chars into chargroup  // Diese Methode gruppiert alle Buchstaben, bei denen der Buchstabe gleich ist in chargroup.
                    where chargroup.Count() > 1 // Filtert alle Elemente aus chargroup heraus, die die Bedingung nicht erfüllen, also nicht eine größere Anzahl als 1 haben.
                    select chargroup) // Gibt den Typ der zurückgegeben Elemente an. Also alle von chargroup, die nicht gefiltert wurden.
                    .Count() // Zählt alle Elemente (chargroup), die im LINQ-Teil davor gefunden wurden.
                    .Equals(0); // Überprüft, ob die Anzahl der Elemente gleich 0 ist.
        }

        /// <summary>
        /// Benutzt LINQ und Lambdas, vergleichbar aufgebaut wie Streams mit Lambdas in Java-Code, um herauszufinden, ob doppelte Buchstaben in einem String sind.
        /// </summary>
        /// <param name="s">Zu testender String</param>
        /// <returns>Ergebnis, ob String keine doppelten Buchstaben hat: Ture = keine, False = mind. einen</returns>
        private static bool CharacterTesterLINQ2(string s)
        {
            return Regex.Replace(s.ToLower(), @"[^a-z]", "") //Der String wird in den lower Case geschrieben und es werden alle Elemente herausgefiltert, die keine Kleinbuchstaben sind. 
                                                             //Das Entfernen der Sonderzeichen könnte nach der Umwandlung in ein Array auch mit einem Where-Filter durchgeführt werden.
                .GroupBy(chars => chars) //Alle gleiche Buchstaben werden gruppiert.
                .Where(chargroup => chargroup.Count() > 1) //Alle Gruppen, die nicht mehr als 1 Element haben werden hier herausgefiltert.
                .Count() // Die Anzahl der nicht herausgefilterten Gruppen wird gezählt.
                .Equals(0); // Es wird geprüft, ob die Anzahl der Gruppen gleich 0 ist.
        }
    }
}
