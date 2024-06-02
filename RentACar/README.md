# Rent a Car Agency - Inovatec

Napisati program koji će simulirati rezervaciju vozila u Rent a car agenciji. Postoje dve vrste vozila, motori i automobili.
Izdaju se dve marke motora Yamaha i Harley, kao i tri marke automobila Mercedes, BMW i Peugeot. Za motore su poznati
potrošnja, model, kubikaža, snaga i tip (Adventure, Heritage, Tour, Roadster, Urban Mobility, Sport), dok su za svaki
automobil poznati model, potrošnja, kilometraža, tip karoserije (Limuzina, Hečbek, Karavan, Kupe, Kabriolet, Minivan, SUV,
Pickup) kao i oprema koju ima. Početna cena za svaki automobil je 200e po danu, dok je početna cena za motore 100e po
danu.

Za svako vozilo cena po danu se računa na sledeći način:
- **Mercedes**
  - ako je prešao manje od 50000km cena se povećava za 6%,
  - ako je limuzina povećava se za 7%,
  - ako je hečbek i prešao je više od 100000km, cena se smanjuje za 3%.
- **BMW**
  - ako troši manje od 7 litara na 100km cena se povećava za 15%,
  - ako troši više od 7 litara na 100km cena se umanjuje za 10%,
  - u suprotnom cena se smanjuje za 15%.
- **Peugeot**
  - ako je limuzina cena se uvećava za 15%,
  - ako je karavan cena se uvećava za 20%,
  - u suprotnom se smanjuje za 5%.
- **Harley**
  - u startu se cena uvećava za 15%,
  - ako ima više od 1200 kubika cena se povećava za 10%, u suprotnom se smanjuje za 5%.
- **Yamaha**
  - u startu se cena uvećava za 10%,
  - zatim ako mu je snaga veća od 180 KS cena se uvećava za 5%, u suprotnom se smanjuje za 10%,
  - ako je Heritage onda se uvećava za 50e, ako je Sport uvećava se za 100e, u suprotnom se smanjuje za 10e.

Napomena: Procenti uvećanja/umanjenja cena se odnose na početnu cenu vozila.

Takođe, na cenu svakog automobila utiče i njegova oprema. Za opremu se zna naziv, cena kao i informacija da li povećava
ili smanjuje cenu automobila. U agenciju dolaze kupci da iznajme željeno vozilo. Za svakog kupca se zna ime, prezime, kao
i koliki budžet za iznajmljivanje ima i u kom period mu je potrebno vozilo. Određeni kupci mogu imati BASIC ili VIP ČLANSKU
KARTICU i mogu ostvariti određeni procenat popusta prilikom iznajmljivanja: 10% za BASIC i 20% za VIP. Jedan kupac može
da pokuša da iznajmi samo jedno vozilo u određenom vremenskom intervalu, dok jedno vozilo više kupaca može pokušati
da iznajmi, s tim što moraju da vode računa, da li je neki kupac već iznajmio to vozilo u tom periodu. Iznajmljivanje se
obavlja samo ako kupac ima dovoljan budžet. Ukoliko dođu istog dana, prvenstvo prilikom iznajmljivanja vozila, imaju VIP,
pa BASIC kupci, dok kupci bez članske kartice, iznajmljuju vozila poslednji. Nakon uspešne rezervacije treba izmeniti budžet
kupca i sačuvati sve uspešne rezervacije u odgovarajućem fajlu (nove_rezervacije.csv – isti format kao rezervacije.csv).
Nakon pokretanja programa učitavaju se podaci o vozilima, opremi, kupcima kao i informacije o tome koji kupac želi da
iznajmi koje vozilo i u kom periodu, zatim se na standardni izlaz ispisuju podaci o vozilima i njihovim cenama, podaci o
korisnicima kao i koji tip članske karitce imaju (ako je imaju uopšte) i koliki su popust ostvarili. Na kraju se simulira
iznajmljivanje. Kupce (Ime i prezime) koji nisu uspeli da ostvare iznajmljivanje ispisati na standardni izlaz, kao i razlog zašto
nisu uspeli da rezervišu vozilo (zauzet termin ili nedovoljan budžet).

Zadatak je potrebno implementirati kao konzolnu aplikaciju u C#.

U prilogu se nalaze primeri ulaznih fajlova:
- vozila.csv - podaci o vozilima.
- kupci.csv - podaci o kupcima.
- oprema.csv - podaci o opremi.
- vozilo_oprema.csv - podaci o tome koje vozilo ima koju opremu.
- rezervacije.csv - podaci o tome koji kupac je rezervisao neko vozilo u nekom periodu.
- zahtevi_za_rezervaciju.csv – podaci o tome koji kupac želi da rezerviše neko vozilo u nekom periodu.
