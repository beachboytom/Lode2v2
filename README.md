# Lodě 2v2

## Autor
Tomáš Och


## Popis hry
Lodě 2v2 jsou konzolová hra inspirovaná klasickou lodní bitvou, upravená pro hru dvou týmů.  
Každý tým má dva hráče – **Radar** a **Střelce**, kteří se střídají v tazích.

Cílem hry je zničit všechny lodě protivníka (celkem 12 políček).  
Vyhrává tým, který jako první zničí všechny lodě soupeře.

---

## Pravidla
- Každý tým má **jednu společnou mapu 10×10**.
- Tým má **4 lodě**:
  - 1 loď délky 2  
  - 2 lodě délky 3  
  - 1 loď délky 4  
- Lodě se rozmístí **náhodně** při spuštění hry.
- Radar:
  - Zadá souřadnici (X, Y)
  - Hra prozkoumá oblast 3×3 okolo zadaného bodu
  - Odpověď je pouze ANO / NE (zda je v oblasti loď)
- Střelec:
  - Zadá souřadnici (X, Y)
  - Hra vyhodnotí zásah nebo minutu

Tým začíná s **12 životy**, což je počet polí všech lodí.  
Každý zásah odečte protivníkovi 1 život.  
Když životy klesnou na 0 → **tým prohrává**.

---

## Ovládání

### Hlavní menu (během tahu)
1. **Ukázat moji mapu** – zobrazí lodě vlastního týmu  
2. **Pokračovat** – provedení akce podle role

### Zápis souřadnic
- Souřadnice se zadávají **od 1 do 10**
- Hra automaticky převádí na index 0–9

### Role
**Radar**
- Prozkoumává oblast 3×3 okolo zadané pozice

**Střelec**
- Střílí na konkrétní souřadnici
- Vyhodnocuje zásah nebo minutu

---

## Struktura programu

### Hlavní třídy

#### `Team`
Obsahuje:
- název týmu („A“ nebo „B“)
- mapu lodí (`Mapa`)
- mapu zásahů (`Zasahy`)
- životy (`Zivoty`)

Slouží jako datový model celého týmu.

#### `Hrac`
Reprezentuje jednoho hráče (Radar nebo Střelec):
- jméno
- role
- odkaz na tým (`Tym`)
- odkaz na nepřátelský tým (`Nepritel`)
- zjednodušení přístupu na mapy (MojeMapa, NepritelMapa, Zasahy)

#### `Program.cs`
Obsahuje:
- hlavní menu
- do-while smyčku pro tahy
- radar a střelbu
- načtení vstupu
- kontrolu konce hry
- inicializaci týmů a hráčů

### Pomocné metody
- `UdelejMapu()` – vytvoří prázdnou mapu
- `RozmistitLode()` – náhodně rozmístí lodě
- `VypisMapu()` – vypíše mapu s barvami a osami
- `Radar()` – oblast 3×3
- `Strelec()` – zásah/minuta

---

## Známé problémy / omezení
- Neexistuje AI (počítač hraje jen proti člověku)
- Hra neřeší vstupy, které nejsou čísla (při zadání textu hra spadne)
- Nelze ukládat nebo načítat rozehranou hru
- Chybí systém statistik
