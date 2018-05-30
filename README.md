Changelog:

#0.6.7(#30)(30.05.2018)

- Dodano zabezpieczenie przed sytuacja gdzie zadna strona nie moglaby wykonac ruchu z powodu ulozenia klejnotow na planszy
- Dodano mozliwosc zdjecia przedmiotu z ekwipunku
- Dodano mozliwosc zamkniecia okna ubrania przedmiotu

#0.6.6(#29)(29.05.2018)

- Zmieniono przedmiot:

- Claws of Hate: +150 DMG, -100% to ALL Resistances (-> -100% Dodge Chance). (Attack Type - Direct)

- Wykryto i usunieto buga powodujacego brak zakonczenia gry po pokonaniu ostatniego przeciwnika

- Wykryto buga gdzie uzyskane przez gracza punkty doswiadczenia nie zapisuja sie odpowiednio jesli gracz nacisnie "Continue" przed zakonczeniem dodawania punktow doswiadczenia do paska

#0.6.5(#28)(29.05.2018)

- Usunieto buga zwiazanego z brakiem wskazowek dla gracza podczas pierwszej tury rozgrywki
- Usunieto buga, gdzie gracz mogl dowolnie przesuwac paski zdrowia (nie mialo to wplywu na rozgrywke)
- Zablokowano rozdzielczosc na 1366x768
- W scenie "Location Selection" nie da sie juz nacisnac na guziki od poziomu jezeli sie nie pokonalo ostatniego przeciwnika z poprzedniego poziomu
- Dodano informacje do tooltipow statystyk: unik - istnieje mozliwosc ujemnego uniku, to wtedy powoduje otrzymanie podwojnych obrazen; regeneracja: jest ograniczona do 1/12 punktow zdrowia
- Gracz podczas level-upa widzi, co uzyskuje - 20% bonus HP i DMG
- Podczas walki tekst oznajmiajacy o turze ma inny kolor podczas tury przeciwnika.
- W scenie "Story" dodano wskazowke jak przejsc do nastepnej sceny wprowadzenia fabularnego
- Dodano nowy modyfikator przedmiotow: 'base' - modyfikuje statystyki omijajac przy tym blokady nalozone na statystyke
- Zmieniono statystyki przedmiotow i przeciwnikow:


- Boots of Much Dodge: 15% Dodge Chance -> 15% Base Dodge Chance
- Energy Core: +50% Purple Resistance. +15% Dodge Chance (-> +10% Base Dodge Chance). -15% (->-15% Base) Regeneration
- First Aid Kit: (Start of Turn) +2(->+1) Regeneration
- Golden Wrench: +50% Orange and Blue Resist (-> +25% Orange and Blue Resist. +10% Base Orange and Blue Resist.) (Weapon Type - Close Combat)
- Lumberjack's Axe: +75% (->+60%) Green Resistance. (Weapon Type - Slashing). Tooltip teraz dobrze pokazuje
- Pneumo Thruster: +12 Strength. -15% (->-15% Base) Dodge Chance. (Weapon Type -> Thrusting)
- Prototype Injector: +15 (-> +10 Base) Regeneration. -40% Red Resistance. Tooltip teraz dobrze pokazuje
- Shuriken: -30% (-> -30% Base) DMG. Reszta tak samo
- Sturdy Hammer: +30% (-> +25%) Red and Purple Resistance. +5 (-> +5 Base) Strength. (Weapon Type - Impact)
- Unidentified Liquid: +15 (-> +15 Base) Strength. -20 (-> -20 Base) Regeneration

- Rownoczesnie z wprowadzeniem modyfikatora 'base' zaostrzono limity na reszte statystyk:
- Regeneracja: -120:120 ->-50:50
- Unik: 0:85 -> 0:50
- Sila: 0:50 -> -40:40
- Odpornosci: -300:75 -> -300:60

- Zmodyfikowano przeciwnikow, i zmniejszono punkty doswiadczenia otrzymywane od nich

#0.6.4(#27)(28.05.2018)

- Wylaczono okno do zmiany rozdzielczosci

- Dodano tekst do sceny "Credits" informujacy o sposobie przejscia do MainMenu

- Dodano 4 nowe przedmioty i zmieniono slownictwo w starych

- Ulepszono typ modyfikatora: Po kazdej sekwencji powyzej 4 -> Po kazdej sekwencji powyzej 2

- Dodano nowe tlo do sceny "Location Selection" i zmieniono wyglad guzikow lokacji

- Dodano nowa scene "Game Over", ktora uruchomi sie po pokonaniu ostatniego przeciwnika. Wyswietli sie komunikat o przejsciu gry, ilosci 'dni' rozgrywki, a po nacisnieciu dowolnego guzika dane zapisane sie wykasuja i nastapi przejscie do sceny "Main Menu"

- Wprowadzono z powrotem panel wskazowek dla gracza. Ponadto w scenie walki pojawil sie guzik "Show/Hide hints" ktory decyduje o wyswietlaniu panelu.

- Zmodyfikowano kilka przedmiotow

- Od teraz postacie nie moga regenerowac/tracic wiecej niz 1/12 swoim punktow zdrowia co ture

#0.6.3(#26)(27.05.2018)

- Dodano fabule do sceny "Story"

- Zmieniono przeliczniki zniszczonych klejnotow na obrazenia. Teraz obrazenia zadawane sa 0 -> 60% mniejsze, zaleznie od rodzaju wyprowadzonego ataku

- Teraz sila daje stanowczo zwiekszony bonus do atakow typu Barrage (scattershot)

- Zmieniono atrybuty czesci przedmiotow

- Zmieniono jeden typ modyfikatora przedmiotu: Po zadaniu minimum 75 punktow obrazen -> Za kazdy dopasowany klejnot w ataku powyzej 3

- Zwiekszono liczbe punktow doswiadczenia dostarczanych przez przeciwnikow, ale zmniejszono wypadajacy z nich lup do 1 przedmiotu

- Raz pokonani przeciwnicy juz nie rzucaja przedmiotow

- Wszyscy przeciwnicy zadaja zwiekszone obrazenia w zaleznosci od poziomu, na ktorym sie znajduja

#0.6.2(#25)(26.05.2018)

- Utworzono 9 nowych przedmiotow

- Przerobiono 4 istniejacych przeciwnikow i dodano 8 nowych

- Wprowadzono twarde limity (capy) na statystyki

- Znaleziono i usunieto buga gdzie postac mogla uniknac obrazen posiadajac 0 szansy na unik

-Ekwipunek samoczynnie sie powieksza

#0.6.1(#24)(26.05.2018)

- Wstawiono ikonki

- Ustalono tytuly czlonkow zespolu na scenie Credits

- Wysprzatano scene Main Menu

- Guzik "New Game" teraz odpowiednio czysci wszystkie informacje

- W scenie "Location Selection" dodano dwie nowe lokacje, po 4 przeciwnikow kazda. Przeciwnikow trzeba jednak dalej wymyslic

- Dodano licznik 'dni' przebytych wewnatrz gry. Licznik sie zwieksza po podjeciu walki, niezaleznie od jej wyniku

- Okno informacji o przeciwniku wyswietla wiecej informacji o nim

- W scenie "Location Selection" dodano guzik "Return to Main Menu"

- W scenie "Fight" dodano guzik "Escape" dzieki ktoremu gracz moze przedwczesnie skonczyc walke i uciec do sceny "Location Selection"

#0.6.0(#23)(25.05.3018)

- Dokonano kompletnego redesignu okienek od wyswietlania statystyk gracza i przeciwnika

- Wytropiono i usunieto bugi zwiazane z zapisywaniem i odczytem poziomu gracza, oraz inne


#0.5.3(#22)(24.05.2018)

- Od teraz gracz moze uzyskiwac kolejne poziomy doswiadczenia po pokonywaniu przeciwnikow. Kazdy przeciwnik bedzie wart ustalona ilosc punktow doswiadczenia.


- Za kazdy poziom poziom gracz bedzie mial zwiekszone punkty zycia (1+poziom/4)^2 razy oraz bedzie zadawal zwiekszone obrazenia o (poziom*15%)

#0.5.2(#21)(24.05.2018)

- Wyczyszczono czesc niepotrzebnych guzikow i okien ze sceny Main Menu

- Teraz ze sceny Fight mozna przejsc do sceny Location Selection poprzez okno zwyciestwa/porazki

- Po pokonaniu przeciwnika gracz uzyskuje lup: dwa przedmioty, ktore automatycznie dodaja mu sie do ekwipunku. Gracz moze potem w scenie "Location Selection" ubrac te przedmioty i wybrac sie na kolejna misje

-Progres gracza jest zapisywany.

-Utworzono 4 przedmioty i 4 przeciwnikow, na potrzeby demo 

-Naprawiono buga gdzie inwentarz gracza nadpisywal stare przedmioty nowymi. Teraz laduje jak powinno

-Wysprzatano i naprawiono wszystkie bledy powstale w wyniku naprawiania buga powyzej

#0.5.1(#20)(23.05.2018)

- Wykryto i naprawiono buga zwiazanego z warunkowym ladowaniem przeciwnikow podczas przejscia ze sceny Location Selection do sceny Fight

- Wykryto i naprawiono buga w algorytmie generujacym krysztaly na planszy. W pewnych przypadkach mozna bylo na planszy znalezc linie po 3 od razu po rozpoczeciu walki

- Rozpoczeto prace nad ekranem zwyciestwa/porazki po ukonczonej walce

#0.5.0(#19)(22.05.2018)

- Polaczono silnik walki z baza gry.

- Stan panelu inwentarza oraz ekwipunku jest zapisywany nawet po wylaczeniu gry lub zmianie sceny na "Fight"

- W scenie "Location Selection" mozna wybrac sobie przeciwnikow. Po wybraniu oraz nacisnieciu "Embark" wybrany przeciwnik sie zaladuje


#0.4.3(#18)(21.05.2018)
-T eraz tooltip wyswietla sie po najechaniu na przedmiot, zamiast po kliknieciu, i znika po zdjeciu kursora

- Teraz gracz moze ubierac przedmioty znajdujace sie w ekwipunku. Po ubraniu przedmioty sa podmieniane.

- (Fix) tooltip juz nie znika po wyswietleniu panelu ubierania ekwipunku

#0.4.2(#17) (20.05.2018)

- Ekwipunek dziala jak zamierzono


#0.4.1(#16) (20.05.2018)

- Zaimplementowano guziki wyswietlajace ubrany ekwipunek

- Po nacisnieciu na guzik wyswietli sie panel z nazwa przedmiotu i jego tooltipem

- Przedmiot juz moze ustawic typ broni. To jest modyfikator typu "static", nazwa - "weaponType"

- Jezeli gracz nie ma ubranego zadnego przedmiotu z modyfikatorem rodzaju broni, to jego rodzaj ataku ustawiony jest na "basic"

- Jesli gracz ma ubrane 2 bronie, to pierwsza z nich sie liczy (nadal trzeba wprowadzic zabezpieczenie blokujace mozliwosc ubrania broni do pierwszego slotu) 

#0.4.0(#15) (19.05.2018)
- Rozpoczeto prace nad widokiem ekwipunku gracza


- Utworzono klase EquipmentManager zajmujaca sie ladowaniem modyfikatorow z przedmiotow dostepnych dla gracza


- Zaimplementowano 4 rodzaje modyfikatorow: "static" jest ladowany raz, na poczatku rozgrywki. "startofturn" jest ladowany na poczatku kazdej tury za wyjatkiem pierwszej. "combo" jest ladowany za kazdym razem kiedy gracz osiagnie combo min.4, to jest gdy funkcja "zniszcz klejnoty i przesun w dol" uruchomi sie czwarty raz z rzedu. "highdamage" jest ladowany za kazdym razem kiedy gracz zada przeciwnikowi min 200 obrazen jednym ulozeniem klejnotow

#14 (16.05.2018)
- Sporzadzono randomizer walk: po uruchomieniu gry albo nacisnieciu guzika atrybuty gracza i przeciwnika oraz ich bronie zostana losowo przyporzadkowane


- Dodano nowa funkcjonalnosc: po otrzymaniu obrazen lub regeneracji punktow zdrowia nad paskami zdrowia wyskoczy znikajacy tekst z ta wartoscia


- Wyrzucono redundantne atrybuty: pancerz, liniowe zwiekszenie obrazen.

-Wprowadzono dwa nowe atrybuty: regeneracja (odnawianie punktow zdrowia na poczatku tury), sila (zwiekszenie przelicznika zniszczonych krysztalow na obrazenia)

-Atrybuty sa przedstawione nieco zwiezlej niz przedtem

- Tymczasowo wyrzucono funkcjonalnosc zapisu statystyk walki, zostanie ona wprowadzona z powrotem pozniej
#13 (15.05.2018)
- Zaimplementowano guziki, za pomoca ktorych po najechaniu na pole powyzej paska zdrowia (zarowno gracza, jak i przeciwnika), mozna zobaczyc ich atrybuty. Na razie jest to bardzo toporne, trzeba zeby bylo przejrzysciej

#12 (14.05.2018)
- Zaczeto prace nad wydzieleniem atrybutow i podzialem klasy PlayerScript

#11 (11.05.2018)
- Utworzono nowe klasy, reprezentujace kazdy rodzaj broni (6). W kazdej z nich znajduje sie cala logika ataku, od zaznaczenia poprzez sprawdzanie warunkow, na animacji konczac
-Utworzono klase Weapon:MonoBehavior, z ktorej dziedziczy kazda zaimplementowana bron.

- (zmiany z feature-attributes) od teraz niszczone krysztaly sa segregowane kolorami, tzn. np. ze zniszczenie 3 krysztalow zielonych i 3 czerwonych nie bedzie skutkowalo tymi samymi obrazeniami co znisczenie 6 krysztalow zielonych

- Wychwycono i naprawiono buga zwiazanego ze sprawdzaniem warunkow wyprowadzenia ataku

#10(08.05.2018)
- Dodano tymczasowe tlo

- Dodano odstep czasowy miedzy wybraniem klejnotu przez komputer a przesunieciem go. Dodatkowo wyswietlaja sie markery podczas ruchu przeciwnika.

- Usunieto migajace zaznaczenia oraz skrypt Utilities.cs. W pozniejszym czasie zostanie on zastapiony dodatkowymi funkcjonalnosciami wewnatrz skryptow od poszczegolnych rodzajow atakow

#9 (07.05.2018)
- Dodano nowy efekt wizualny: po udanym wyprowadzeniu ataku 'doubleattack' nastapi animacja symbolizujaca ciecie wzdluz wybranej osi po wybranej pozycji
- Dodano rowniez efekty wizualne dla atakow 'hammer' i 'scattershot'. Dla reszty atakow one nie beda potrzebne

- Naprawiono buga zwiazanego z blednym wyswietlaniem markerow w niektorych przypadkach

- Dodano mozliwosc odznaczenia wybranego klejnotu za pomoca klikniecia prawym przyciskiem myszy

- Dodano wskazowki dla gracza odnosnie wyprowadzania ruchow

- Od teraz dane odnosnie statystyk atakow beda sie zapisywac po nacisnieciu lewego klawisza Alt (w zamysle jako czesc sekwencji Alt+F4)

- Od teraz nie mozna klikac klejnotow na planszy podczas tury przeciwnika

#8 (07.05.2018)
- Dane potrzebne do balansowania roznych typow atakow sa przechowywane i zapisywane. Po nacisnieciu guzika ESC (WAZNE) dane sie zapisza (a wewnatrz Builda gra sie samoczynnie zgasi), a w folderze gry zostanie wygenerowany plik 'data.txt' przechowujacy wartosci liczbowe dla: typow atakow obydwu stron oraz (liczby ich wyprowadzenia, obrazen zadanych atakiem oraz obrazen zadanych spadajacymi klejnotami)

-Hotfix: Wyzej wspomniane dane mozna zresetowac guzikiem "Clear Damage Data". On zmienia wszystkie zapisane dane odnosnie obrazen na 0 i zezwala na kontynuowanie zapisu danych (wskazane podczas modyfikacji przelicznikow)

#7 (06.05.2018)
- Zaimplementowano markery. Teraz gracz bedzie wiedzial gdzie moze przemiescic klejnoty (w przypadku basic,dragthrough) lub ktore klejnoty moga zostac zniszczone w wyniku jego ruchu (w przypadku hammer, scattershot,doubleattack). Nie implementowano dla dragndropa kdyz to niepotrzebne

- wykryto i usunieto buga w ataku 'dragthrough' powodujacego brak animacji dla klejnotow przemieszczonych z kolumny 0 podczas ataku

- dodano tag w widocznym miejscu oznajmiajacy o tym czyja tura sie obecnie rozgrywa

- Dodano mozliwosc zrestartowania walki z nowymi bronmi. Guzik "Change Encounters" pozwala na zmiane broni gracza oraz przeciwnika. Po nacisnieniu "Confirm" bronie zostana zmienione, a paski zdrowia uzupelnione

-Wykryto buga: jezeli po kliknieciu na dowolne miejsce na planszy kliknie sie na klejnot, markery nie zostana wygenerowane. To sie stanie dopiero po drugim kliknieciu na klejnot nie bedacy czescia ataku (atak zostanie przeprowadzony normalnie, nawet bez markerow)

- Wykryto buga: pierwsze klikniecie gry rejestrowane jest podwojnie, z jakiegos niewiadomego powodu

#6 (04.05.2018):
- Zaimplementowano nowy atak: zniszcz wszystkie klejnoty wzd³u¿ osi, które s¹ tego samego typu co ten zaznaczony. AI ju¿ go umie

#5 (04.05.2018):
- Zaimplementowano nowy atak: 'dragthrough'. Przeciagnij klejnot wzdluz wybranej osi, zmieniajac przy tym pozycje wszystkich klejnotow po drodze

- AI juz umie 'dragthrough'

#4 (04.05.2018)
- AI juz umie 'basic'

#3 (03.05.2018)
- AI juz umie 'dragndrop'

#2 (03.05.2018)
- Zaimplementowano sztuczna inteligencje dla przeciwnika. Wrog jest w stanie korzystac z ruchow typu "hammer" i "scattershot". Pomiedzy koncem ataku gracza i wyprowadzeniem ruchu przez przeciwnika jest odstep 0.5 sekundy

- Istnieje koniecznosc balansowania roznych typow atakow. W celu testowania wartosci przelicznikow zalecane jest wprowadzenie bardzo duzych ilosci punktow zdrowia dla gracza oraz przeciwnika oraz poprowadzenie rozgrywki. Przeliczniki rozbitych klejnotow na obrazenia znajduja sie na poczatku funkcji public IEnumerator BreakMatchesAndGravity(IEnumerable<GameObject> totalMatches), zmienne odpowiedzialne to 'damage' i 'match_damage'


#1 (02.05.2018)
- Zaimplementowano nowy rodzaj ataku: Zniszcz wybrany klejnot i 2 inne, losowe, o tym samym kolorze (Oryginalnie planowano 4 inne, ale atak w takiej formie skutkowal zbyt duza iloscia kaskadowych dopasowan, to by³o zbyt OP (overpowered)

- Update: wykryto i usunieto bug zwiazany z proba zniszczenia typ rodzajem ataku mniejszej liczby klejnotow niz to mozliwe, skutkujacy zapetleniem gry


-Zaimplementowano rozne przeliczniki zniszczonych klejnotow na zadawane obrazenia. Przeliczanie znajduje sie wewnatrz skryptu ShapesManager, w funkcji BreakMatchesAndGravity (Dwie funkcje po³aczone w jedno. To by³o konieczne)

- (W koncu dzia³a) Zaimplementowano 'podswietlenie' klejnotu zaznaczonego przez gracza

- skrypty ataku sa teraz statyczne.

#?? (01.05.2018)
- Zaimplementowano nowy rodzaj ataku: Zamien dwa dowolne klejnoty miejscami
- Naprawiono bug zwiazany z atakami typu 3 w linii
- Naprawiono bug uniemozliwiajacy wybor nowych klejnotow w niektorych rodzajach atakow
- 
#??-1 (30.04.2018)
- Zaimplementowano rodzaj ataku: Zaznacz kwadrat 3x3 i zniszcz wszystkie klejnoty tego samego koloru co ten zaznaczony na srodku. Sposob wyprowadzenia: kliknij na 1 klejnot 2 razy
