Memory Game

Апликацијава е едноставна имплементација на игра со мемроија. Кога се стартува на корисникот му е понудено мени со избор на три нивоа односно три големини - 2 x 4, 4 x 4 и 4 x 5. Секое од нивоата ја има соодветната големина и различно време за играње. На почетокот од секоја партија на корисникот му се прикажани сите полиња како отворени со времетраење од 5 секунди, а по истекување на 5те секунди тие се затвараат и играта започнува. Може да се одберат само две полиња во исто време, и доколку тие не се со иста слика, за половина секунда се затвараат и се одземаат дополнителни 5 секунди од вкупното време ( penalty ). Доколку тие се со иста слика остануваат отворени. Целта е да се погодат сите парови со исти слики за даденото време. Доколку тоа не се оствари во даденото време, играта е загубена. Ако корисникот го победи истото ниво 2 пати му се нуди опција да премине на потешко ниво. (ова важи за првото и второто ниво, во третото може да избере дали да игра уште на трето или да почне од прво)


Kодовите за нивоата се разликуваат само во бројот на полиња кои треба да се отворат и вредноста на бројачот. Полињата се всушност имплементирани преку класа Frame во која се чуваат копчиња (buttons), а сликите се поставуваат како слики на копчињата.
Во секој објект од Frame освен копче и слика на копчето се чува и променлива која означува дали е селектирано полето и променлива која означува дали е погодено полето. Исто така има и соодветен конструктор, и методи за прикажување и затворање на секое поле. Во конструкторот се иницијализираат слика и копче, се означува дека иницијално не е селектирано ниту погодено, се задава Image Tag за да се индетифицира секое поле со својот пар и се позадинска слика на копчето, за подобри визулени ефекти.
Програмата користи два бројача. Едниот е за почетните 5 секунди во кои се прикажуваат сликите (се местат сликите од Image делот на копчето во Background Image на копчето) и има интервал од 1 секунда, додека пак за тоа време другиот бројач има интервал од 5 секунди. Кога ќе отчука вториот првиот бројач стопира, а вториот ќе го промени интервалот во 1 секунда. Во истото отчукување сликите ќе се “свртат“, односно ќе се смени позадинската слика на копчето во прашалник.
Во кодот на секоја форма за ниво се чува листа од сите полиња/рамки кои ќе се исцртаат на формата. Има сегмент кој ги размешува сликите со помош на Random. (ова можеше и да се имплементира како посебна функција) Има една променлива која означува колку полиња се во моментот селектирани за да не се дозволува да се селектираат повеќе од две полиња последователно.
Има и променлива која брои победи и одкако ќе има 2 победи на тековното ниво нуди опција за преминување на следно ниво. Исто така преоптоварена е клик функцијата, која што при секој клик проверува дали бројот на отворени копчиња е еднаков на два, и доколку да проверува дали тие се пар преку Image Tag на сликата во копчето.


Изгледот на играта:
Оваа форма се отвара за да одберете ниво:

<img src="http://i.imgur.com/86xaIUL.jpg"/>

Доколку изберете 2 x 4, се отвара овој изглед:
<img src="http://i.imgur.com/3hYwQoj.jpg"/>

Доколку изберете 4 x 4, се отвара овој изглед:
<img src="http://i.imgur.com/PTMl1tK.jpg"/>

Доколку изберете 4 x 5, се отвара овој изглед:
<img src="http://i.imgur.com/yRu6QPr.jpg"/>

Доколку во менито изберете да играте нова игра се појавува овој прозорец:

<img src="http://i.imgur.com/tDpwk2z.jpg"/>

Доколку победите се појавува овој прозорец:

<img src="http://i.imgur.com/LUaSBPk.jpg"/>

После победата се појавува прозорецот за нова игра.

Доколку изгубите се појавува овој прозорец:

<img src="http://i.imgur.com/XK615ek.jpg"/>

Правила на играта: 
                                                                                                      
1. На почеток се избира ниво на игра.

2. Ги гледате сите полиња како отворени за да добиете некој head start. 

3. Селектирате најмногу две полиња одеднаш и пробувате да ги запомните сите сликички на која позиција се наоѓаат.

4. Доколку полињата не се со иста вредност тие се затвораат и ви се одзимаат 5 секунди од бројачот, доколку не тие остануваат отворени.

5. Целта е да ги спарите сите полиња.

6. Доколку не завршите во даденскиот временски рок и правите премногу грешки, губите.
