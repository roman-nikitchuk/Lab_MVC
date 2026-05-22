# LibraryHub - System zarządzania cyfrową biblioteką

## Spis treści
1. [Opis projektu](#opis-projektu)
2. [Funkcjonalności](#funkcjonalności)
3. [Instrukcja uruchomienia](#instrukcja-uruchomienia)
4. [Uruchomienie w Docker](#uruchomienie-w-docker)

## Opis projektu

System zarządzania cyfrową biblioteką zbudowany w oparciu o wzorzec MVC przy użyciu ASP.NET Core. 
Aplikacja umożliwia zarządzanie książkami, autorami, gatunkami, użytkownikami oraz wypożyczeniami.

## Funkcjonalności

- Książki - dodawanie, edytowanie, usuwanie i przeglądanie książek z tytułem, autorem, gatunkiem, rokiem wydania i liczbą egzemplarzy
- Autorzy - zarządzanie autorami z imieniem i krajem pochodzenia
- Gatunki - zarządzanie gatunkami literackimi
- Użytkownicy - zarządzanie użytkownikami biblioteki z imieniem, emailem i hasłem
- Wypożyczenia - śledzenie wypożyczeń z automatycznym statusem Active, Returned, Overdue
- Wyszukiwanie - wyszukiwanie książek po tytule, autorze lub gatunku
- Walidacja - walidacja danych po stronie serwera i klienta

## Instrukcja uruchomienia

### Wymagania
- .NET 8 SDK
- Docker Desktop

### Kroki

1. Sklonuj repozytorium

    git clone https://github.com/roman-nikitchuk/Lab_MVC.git
    cd Lab_MVC

2. Uruchom bazę danych

    docker run --name library-postgres -e POSTGRES_PASSWORD=password -e POSTGRES_DB=LibraryDb -p 5432:5432 -d postgres

3. Zainstaluj pakiety

    dotnet restore

4. Zastosuj migracje

    dotnet ef database update

5. Uruchom aplikację

    dotnet run

6. Otwórz przeglądarkę pod adresem który pojawi się w terminalu (http://localhost:5291)



### Używane pakiety
- Microsoft.EntityFrameworkCore
- Npgsql.EntityFrameworkCore.PostgreSQL
- Microsoft.EntityFrameworkCore.Tools

## Uruchomienie w Docker

### Wymagania
- Docker Desktop

### Kroki

1. Sklonuj repozytorium

    git clone https://github.com/roman-nikitchuk/Lab_MVC.git
    cd Lab_MVC

2. Uruchom aplikację

    docker-compose up --build

3. Otwórz przeglądarkę

    http://localhost:8080