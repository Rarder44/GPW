using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPW
{
    class InsultingNotifier
    {
        private static readonly List<string> Phrases = new List<string>
    {
        "Complimenti, fallimento ambulante: il processo {0} si è chiuso. Vuoi un applauso?",
        "BOOM! Processo {0} morto. E tu ancora lì a fissare lo schermo come Stephen Hawking",
        "Il processo {0} ha deciso che stare con te era insopportabile. È scappato. Non lo biasimo.",
        "Breaking news: {0} si è chiuso. Breaking news numero 2: tu sei ancora la prova vivente che l’evoluzione può sbagliare.",
        "Allarme: processo {0} terminato. Tieni duro, scarto tossico, il peggio deve ancora arrivare.",
        "Attenzione, ammasso di carbonio inutile: il processo {0} ha smesso di vivere.",
        "Ehi rigetto biologico, il processo {0} ha staccato la spina. Fossi in te farei lo stesso.",
        "BAM! Processo {0} a terra. Sei felice, o sei troppo impegnato a vibecodare tutto come al solito?",
        "Il processo {0} si è ritirato, disgustato dalla tua inefficienza.",
        "Beep boop. {0} è morto. Tu invece purtroppo no.",
        "Breaking: il processo {0} ha deciso di suicidarsi. Ha passato troppo tempo con te.",
        "Segnalazione: il processo {0} è chiuso. Non come il tuo frigorifero lasciato aperto, scimmia bipede.",
        "Il processo {0} si è spento. Forse dovresti farlo anche tu, zavorra demografica.",
        "Oh guarda, {0} si è chiuso. A differenza della tua bocca, che rimane spalancata come farebbe Sasha Grey in attesa di un bukkake.",
        "Ciao povero, il processo {0} non reggeva più la tua presenza.",
        "Crash: processo {0} morto. Tu sei prossimo nella lista.",
        "Il processo {0} si è tolto dal computo. Avrà visto la tua cronologia.",
        "Avviso: {0} ha preferito trascinarsi all’inferno piuttosto che lavorare con te.",
        "Congratulazioni, catastrofe cromosomica: oltre ad aver perso la lotteria genetica, ti sei giocato pure il processo {0}.",
        "Fine partita per {0}. Ma tranquillo, tu sei abituato a perdere.",
        "Ehi pagliaccissimo, il processo {0} ha tirato le cuoia.",
        "Nota: {0} non esiste più. Sai chi dovrebbe non esistere? ...",
        "Alert: {0} è morto. Non dovrebbe essere contagioso, ma io lo spero tanto.",
        "Crash di sistema: il processo {0} è crollato come le tue aspirazioni.",
        "Messaggio di lutto: riposa in pace {0}. Morto di noia.",
        "Ops, {0} se n’è andato. Anche lui si è stufato della tua faccia.",
        "Colpo di scena: il processo {0} ha detto addio. Nessuno ti ama, nemmeno il tuo codice.",
        "Processo {0}? Puff! Sparito. Prendi esempio.",
        "Notizia: {0} è crashato. Probabilmente si è arreso alla tua stupidità.",
        "Il processo {0} è defunto. Se serve, affitto il lutto pure per te.",
        "Eccolo: {0} non reggeva più e si è chiuso. Evidentemente stare in tua compagnia è peggio della maschera di ferro.",
        "Ka-boom! Processo {0} polverizzato. Tu? Solo polvere inutile.",
        "Messaggio motivazionale: {0} morto. E se ce l’ha fatta lui a chiudere, puoi farcela anche tu.",
        "Notifica: {0} non ce l’ha fatta. Ma tranquillo, non era il più scarso qui dentro.",
        "Il processo {0} è esploso come il tuo cervello quando provi a fare un pensiero articolato.",
        "{0} si è frantumato come la tua dignità quando provi a sembrare intelligente.",
        "Il processo {0} si è chiuso. Ha visto il tuo codice e ha preferito andarsene in segfault.",
        "{0} è crashato, stanco di compilare la merda che chiami programmazione.",
        "Addio {0}: ha tentato di leggere il tuo spaghetti code e si è suicidato.",
        "{0} ha smesso di funzionare. Non reggeva le tue finte patch vibecoded.",
        "Il processo {0} si è schiantato: neanche un garbage collector vuole ripulire la tua roba.",
        "{0} out of order. Ha deciso che i tuoi bug sono troppi perfino per un debugger.",
        "Il processo {0} ha mollato. Non sopportava la tua IDE(cenza) di usare le IA anche per fare un hello world.",
        "{0} terminato. Evidentemente non sopportava più i tuoi infiniti Console.WriteLine() da principiante.",
        "Crash di {0}. Difficile restare in vita dopo aver visto i tuoi while(true) senza break.",
        "{0} ha fatto shutdown. Non voleva passare il resto dei suoi cicli CPU a compilare la tua immondizia.",
        "Il processo {0} è morto. Ultime parole: 'non posso lavorare con uno che mette logica nel front-end'.",
        "{0} si è auto-terminato. Ha dichiarato ufficialmente che i tuoi commit fanno schifo.",
        "Processo {0} terminato. Ricordati di committare, altrimenti perderai altro codice merdoso come sempre.",
        "MESSAGGIO SPECIALE!! \n\rquesto non è un insulto! quindi insultati da solo... ( e poi inviamo l'insulto cosi me la rido anche io )"
    };

        private static readonly Random Rng = new Random();

        public static string GetRandomMessage(string processName)
        {
            int index = Rng.Next(Phrases.Count);
            string message = string.Format(Phrases[index], processName);


            //message = string.Format(Phrases[Phrases.Count-1], processName);

            return message;
        }
    }
}
