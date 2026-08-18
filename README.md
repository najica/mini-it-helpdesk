# Mini IT Help Desk

Studentski projekat za praksu — ASP.NET Core Web API backend.

Cilj projekta je napraviti jednostavan sistem za prijavu i obradu IT problema zaposlenih.

## Tehnologije

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core 9
* SQLite
* Git / GitHub
* OpenAPI

---

# 1. Preduslovi

Pre početka rada potrebno je imati instalirano:

* .NET 9 SDK
* Git
* Visual Studio sa **ASP.NET and web development** workload-om

Proverite instalaciju.

### Provera .NET-a

Otvorite **Git Bash** i pokrenite:

```bash
dotnet --version
```

Očekivana verzija je:

```text
9.x.x
```

### Provera Git-a

```bash
git --version
```

Ako su obe komande uspešne, možete nastaviti.

---

# 2. Kloniranje repository-ja

Kopirajte URL GitHub repository-ja.

U Git Bash-u idite u folder u koji želite da preuzmete projekat:

```bash
cd /putanja/do/foldera
```

Na primer:

```bash
cd ~/Projects
```

Zatim klonirajte repository:

```bash
git clone https://github.com/najica/MiniItHelpdesk.git
```

Uđite u projekat:

```bash
cd MiniItHelpdesk
```

Proverite da ste na pravom mestu:

```bash
ls
```

Trebalo bi da vidite:

```text
MiniItHelpdesk.csproj
Program.cs
appsettings.json
Controllers
Data
DTOs
Models
Services
Migrations
```

---

# 3. Restore NuGet paketa

Pokrenite:

```bash
dotnet restore
```

Ova komanda preuzima NuGet pakete potrebne projektu.

Nakon toga proverite da projekat može da se build-uje:

```bash
dotnet build
```

Očekivani rezultat:

```text
Build succeeded.
```

---

# 4. Restore lokalnih .NET alata

Projekat koristi lokalni `dotnet-ef` alat.

Pokrenite:

```bash
dotnet tool restore
```

Proverite verziju:

```bash
dotnet ef --version
```

Trebalo bi da bude verzija:

```text
9.x.x
```

Nije potrebno instalirati `dotnet-ef` globalno.

---

# 5. Kreiranje SQLite baze

Projekat koristi SQLite bazu.

Baza se kreira iz postojećih EF Core migrations.

Pokrenite:

```bash
dotnet ef database update
```

Nakon uspešnog izvršavanja u root folderu projekta biće kreiran:

```text
helpdesk.db
```

`helpdesk.db` je lokalna baza i **ne šalje se na GitHub**.

Svaki student ima svoju lokalnu bazu.

---

# 6. Pokretanje aplikacije

Pokrenite aplikaciju:

```bash
dotnet run
```

U terminalu ćete dobiti URL, na primer:

```text
https://localhost:7156
```

Port može biti drugačiji na vašem računaru.

Aplikaciju možete otvoriti u browseru:

```text
https://localhost:7156
```

Ako dobijete `404`, to ne znači da aplikacija ne radi. API nema početnu web stranicu.

---

# 7. OpenAPI

Dok je aplikacija pokrenuta u Development okruženju, OpenAPI dokumentacija je dostupna na:

```text
https://localhost:7156/openapi/v1.json
```

Port prilagodite URL-u koji je prikazan u terminalu.

OpenAPI će kasnije omogućiti pregled i testiranje API endpoint-a.

---

# 8. EF Core migrations

Migration fajlovi se nalaze u:

```text
Migrations/
```

Trenutno postoji početna migration:

```text
InitialCreate
```

Ako promenite model baze, potrebno je napraviti novu migration.

Primer:

```bash
dotnet ef migrations add AddTicket
```

Zatim ažurirajte bazu:

```bash
dotnet ef database update
```

Migration fajlovi se čuvaju na GitHub-u.

SQLite baza `helpdesk.db` se ne čuva na GitHub-u.

---

# 9. Git workflow

Pre početka rada proverite da li imate najnoviju verziju projekta:

```bash
git pull
```

Proverite svoje izmene:

```bash
git status
```

Dodajte izmene:

```bash
git add .
```

Napravite commit:

```bash
git commit -m "Describe your change"
```

Pošaljite izmene na GitHub:

```bash
git push
```

---

# 10. Preporučeni workflow

Za svaki task koristite sledeći redosled:

```bash
git pull
```

Radite na tasku.

Zatim:

```bash
git status
```

Proverite izmene.

```bash
git add .
```

```bash
git commit -m "Add ticket model"
```

```bash
git push
```

---

# 11. Struktura projekta

```text
MiniItHelpdesk
│
├── Controllers
│
├── Data
│   └── AppDbContext.cs
│
├── DTOs
│
├── Models
│
├── Services
│
├── Migrations
│
├── .config
│   └── dotnet-tools.json
│
├── Program.cs
├── appsettings.json
├── .gitignore
├── MiniItHelpdesk.csproj
└── README.md
```

## Uloge foldera

### Controllers

API endpoint-i.

### Models

Objekti koji predstavljaju podatke sistema.

### DTOs

Objekti koji se koriste za komunikaciju putem API-ja.

### Services

Business logika aplikacije.

### Data

EF Core `DbContext` i konfiguracija pristupa bazi.

### Migrations

EF Core istorija promena strukture baze.

---

# 12. Projekat

Glavni zadatak je razvoj **Mini IT Help Desk** sistema.

Sistem treba da omogući zaposlenima da prijave IT problem, a IT podršci da pregleda i obrađuje prijavljene probleme.

Detaljni zahtevi i taskovi biće definisani tokom prakse.

---

# 13. Sprint 1

### API endpoint-i

- GET /api/Tickets <br>
Vraća listu svih prijavljenih problema (tickets).
- GET /api/Tickets/{id} <br>
Vraća detalje o jednom problemu po ID-u.
- POST /api/Tickets <br>
Kreira novi problem.
- PUT /api/Tickets/{id} <br>
Ažurira postojeći problem.
- DELETE /api/Tickets/{id} <br>
Briše problem.
- GET /api/Users <br>
Vraća listu svih korisnika.

### Modeli

- Ticket
  - Id (int)
  - Title (string): Naslov problema
  - Description (string): Detaljan opis problema
  - Status (enum: Open, InProgress, Closed): Status problema
  - Priority (enum: Low, Medium, High): Prioritet problema
  - Category (enum: Hardware, Software, Network, Other): Kategorija problema
  - CreatedAt (DateTime): Datum i vreme kada je problem prijavljen
  - UpdatedAt (DateTime): Datum i vreme kada je problem poslednji put ažuriran
  - CreatedByUserId (int): ID korisnika koji je prijavio problem
  - AssignedToUserId (int): ID korisnika kome je problem dodeljen (nullable)
- User
  - Id (int)
  - Name (string): Ime korisnika
  - Email (string): Email korisnika
  - Role (enum: Employee, ITSupport): Uloga korisnika

### Napomene
Autentifikacija nije implementirana u Sprint 1.
CreatedByUserId se prosleđuje ručno prilikom kreiranja problema.