Introduzione generale al progetto
1) applicazione creata con il framework ASP.net core
2) db creato con sql server e mappato tramite entity framework
3) ui in scss, HTML e javascript

Spiegazione sullìimplementazione delle specifiche 
1) "I campi Nome, Cognome, Sesso, Email dovranno essere obbligatori" -> campi resi non nullable nel db, e required nella rispettiva classe Contatto.cs
2) "Il campo Email dovrà contenere una mail valida" -> validazioneb tramite il tag     [EmailAddress]
3) "Il campo Numero di telefono dovrà contenere un numero di telefono valido" -> validazione tramite regex, è stato considerato valido una stringa composta solo da numeri, da 5 a 15 caratteri
4) "I dati dovranno essere archiviati in un database SQL" -> la tabella 'Contatto' puo essere creato tramite la query allegata nel file 'Contatto.sql'
5) "Sesso (da casella a discesa M/F)" -> il tipo di questo campo è un enum
6) "Città (da casella a discesa con elenco di alcune città presenti in una tabella da pre-popolare esternamente all'applicazione)"->ho creato nel db una tabella 'Città' che verrà interrogata tramite apposita APi. La tabella 'Città' puo essere creato tramite la query allegata nel file 'Città.sql'
7) è stata scelta come chiave della tabella Contatto, il campo 'Mail', perchè è obbligatorio, e di solito univoco.
