## 1. Opis działania aplikacji

CarShop to platforma do przeglądania i zamawiania samochodów. Użytkownik końcowy może korzystać z:

### Przeglądanie samochodów
- Użytkownicy mogą przeglądać dostępne samochody, korzystając z filtrów, aby znaleźć odpowiedni model.

### Składanie zamówień
- Użytkownicy mogą składać zamówienia na wybrane samochody. Formularz zamówienia zawiera wszystkie niezbędne informacje.

### Zarządzanie zamówieniami
- Użytkownicy mają dostęp do historii swoich zamówień, gdzie mogą edytować dane dotyczące zamówień lub je usuwać oraz opłacić.

### Panel użytkownika
- Użytkownicy mogą przeglądać informacje o swoim koncie

### Logowanie
- Użytkownicy mogą się logować, podając nazwę użytkownika i hasło. Aplikacja obsługuje prostą walidację, aby zapewnić bezpieczeństwo.

### Rejestracja
- Użytkownicy mogą zarejestrować się, tworząc nowe konto w systemie.

## 2. Wymagania systemowe

- **System operacyjny**: Windows, macOS, Linux
- **.NET SDK**: Wersja 8.0 lub nowsza
- **SQLite**: Wersja 3.0 lub nowsza
- **IDE**: Visual Studio, Visual Studio Code lub inny edytor obsługujący C#

## 3. Instalacja

1. **Klonowanie repozytorium**
   ```bash
   git clone https://github.com/TwojeRepozytorium/CarShop.git
   ```
lub polecam zrobić to przez Visual Studio, najszybszy sposób

2. **Migracje bazy danych**
   Aby utworzyć bazę danych, uruchom polecenie:
   ```bash
   dotnet ef database update
   ```
lub skorzystać z pliku z repozytorium.

3. Wpisanie **dotnet run** albo uruchomić przez Visual Studio 

## 4. Konfiguracja

### Łańcuch połączenia z bazą danych

Do połączenia z bazą danych jest fragment kodu z program.cs. Jest tam zdefiniowane jaka nazwa bazy danych ma być użyta
```json
Data Source=DB.db
```

### Testowi użytkownicy

- **Użytkownik**: `admin`
- **Hasło**: `admin`
-
- **Użytkownik**: `user`
- **Hasło**: `user`
