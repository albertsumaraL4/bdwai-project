# BDWAI – system zarządzania ankietami

Projekt wykonany w ramach zajęć.

Jest to prosta aplikacja webowa do zarządzania ankietami — administrator tworzy ankiety, a użytkownicy mogą je wypełniać. Aplikacja udostępnia również statystyki wyników, które można filtrować m.in. po wieku i lokalizacji respondentów.

---

## Technologie
- **C#**
- **ASP.NET**
- **Entity Framework**
- **ASP.NET Identity**
- **SQL Server (SSMS)**

---

## Jak działa aplikacja

### Użytkownicy i role
Aplikacja posiada system rejestracji i logowania oparty o **ASP.NET Identity**.  
Występują dwie role:
- **Administrator** – zarządza ankietami i ma dostęp do pełnych statystyk,
- **Użytkownik** – może wypełniać ankiety i przeglądać wyniki.

Dostęp do funkcjonalności jest ograniczony w zależności od roli użytkownika.

---

### Ankiety
- ankiety są tworzone przez administratora,
- użytkownicy mogą wypełniać dostępne ankiety online,
- odpowiedzi zapisywane są w relacyjnej bazie danych.

---

### Statystyki
- statystyki są dostępne dla wszystkich użytkowników,
- wyniki można filtrować:
  - według **lokalizacji**,
  - według **wieku** respondentów,
- celem było pokazanie praktycznego wykorzystania zapytań do bazy danych w aplikacji webowej.

---

## Baza danych
Aplikacja korzysta z **SQL Server**, a struktura bazy była tworzona i zarządzana przy użyciu **SQL Server Management Studio (SSMS)**.  
Dane aplikacyjne oraz dane użytkowników są obsługiwane przez Entity Framework.

---

## Cel projektu
Celem projektu było praktyczne połączenie:
- relacyjnej bazy danych z aplikacją webową,
- systemu logowania i ról użytkowników,
- logiki biznesowej związanej z ankietami i analizą danych.

---

## Status projektu
Projekt został zakończony w ramach zajęć i nie jest obecnie rozwijany.  
Repozytorium zostało zarchiwizowane.

