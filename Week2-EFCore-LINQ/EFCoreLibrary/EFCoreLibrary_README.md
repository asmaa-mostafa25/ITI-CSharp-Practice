# 🗄️ Entity Framework Core & LINQ — Code First vs Database First

Solution فيه **مشروعين** بيوضحوا أشهر طريقتين تتعامل بيهم مع قاعدة بيانات في **Entity Framework Core**:

1. **Part 1 — Code First**: بتبني قاعدة البيانات من الكلاسات بتاعتك.
2. **Part 2 — Database First**: بتاخد قاعدة بيانات موجودة بالفعل (Northwind) وتولّد منها كلاسات #C.

---

## 📌 نظرة عامة

الهدف من الـ Solution إنك تتمرن على التعامل مع قواعد البيانات العلائقية بأسلوبين مختلفين تمامًا، وتستخدم **LINQ** عشان تستعلم على البيانات في الحالتين.

---

## 🛠️ الأدوات المستخدمة

- C# / .NET / Visual Studio 2022
- Entity Framework Core + SQL Server
- LINQ
- EF Core Migrations *(في Part 1)*
- EF Core Power Tools *(في Part 2)*

### NuGet Packages (Part 1)
```text
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design
```

---

## 📂 هيكل الـ Solution

```text
Solution
│
├── Part 1 - Code First
│   ├── Data/LibraryContext.cs
│   ├── Models/Author.cs, Book.cs
│   ├── Migrations/
│   └── Program.cs
│
└── Part 2 - Database First
    ├── Models/ (Customer, Order, Product, Employee, ...)
    ├── DbContext/DESKTOPMS535M4SQLEXPRESSContext.cs
    └── Program.cs
```

---

# 1️⃣ Part 1 — Code First (Library)

نفس فكرة مشروع المكتبة: `Author` عنده أكتر من `Book` (علاقة **One-to-Many**)، والكود هو اللي بيولّد قاعدة البيانات.

### أهم المفاهيم

#### Entity Models + Navigation Properties
```csharp
public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public ICollection<Book> Books { get; set; }
}

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int AuthorId { get; set; }   // Foreign Key
    public Author Author { get; set; }  // Navigation Property
}
```
`AuthorId` جوه `Book` هي الـ **Foreign Key** اللي بتربط كل كتاب بمؤلفه، و`Author`/`Books` هما **Navigation Properties** بتخليك تتنقل بين الكائنات مباشرة في الكود من غير JOIN يدوي.

#### DbContext
```csharp
public DbSet<Author> Authors { get; set; }
public DbSet<Book> Books { get; set; }
```
`LibraryContext` هو المسؤول عن كل التواصل مع SQL Server، وكل `DbSet<T>` بيمثل جدول.

#### EF Core Migrations
```powershell
Add-Migration InitialCreate
Update-Database
```
`Add-Migration` بتحسب الفرق بين شكل الكلاسات الحالي والـ Migration اللي فاتت، و`Update-Database` بتنفذ الفرق ده فعليًا على SQL Server وتنشئ الجداول. ده جوهر **Code First**: الكود هو المصدر، وقاعدة البيانات بتتبعه.

#### إضافة بيانات مرتبطة
```csharp
var author = new Author
{
    Name = "John Doe",
    Books = new List<Book> { new Book { Title = "C# Programming", Price = 29.99m } }
};
context.Authors.Add(author);
context.SaveChanges();
```
الكتب اتضافت جوه الـ `Books` Collection بتاعة المؤلف، فلما تنادي `SaveChanges()`، EF Core بيحفظ المؤلف وكتبه مع بعض ويربط الـ Foreign Key تلقائيًا.

#### قراءة البيانات بـ Eager Loading
```csharp
var authors = context.Authors.Include(a => a.Books).ToList();
```
`Include()` بتقول لـ EF Core "هات المؤلف وكتبه مع بعض في نفس الاستعلام" (**Eager Loading**)، و`ToList()` هي اللي بتنفذ الاستعلام فعليًا وترجع النتيجة كـ List.

---

# 2️⃣ Part 2 — Database First (Northwind)

هنا العكس تمامًا: قاعدة البيانات (**Northwind**) كانت موجودة بالفعل، والكلاسات اتولّدت منها.

### Reverse Engineering (Scaffolding)
```text
EF Core Power Tools
        ↓
Reverse Engineer
        ↓
Connect to SQL Server
        ↓
Select Northwind Database
        ↓
Select Tables
        ↓
Generate C# Models + DbContext
```
أداة **EF Core Power Tools** بتقرا هيكل قاعدة البيانات الموجودة (جداولها وعلاقاتها) وتولّد تلقائيًا:
- كلاسات Entity بتمثل كل جدول
- Navigation Properties بتمثل العلاقات
- `DbContext` كامل جاهز

العملية دي اسمها **Reverse Engineering** أو **Scaffolding** — عكس Code First تمامًا.

### استعلامات LINQ المستخدمة

#### 1. Filtering — `Where()`
```csharp
var products = context.Products
    .Where(p => p.UnitPrice > 50)
    .ToList();
```
`Where()` بتفلتر السجلات بناءً على شرط — هنا هات بس المنتجات اللي سعرها أكتر من 50.

#### 2. Eager Loading + `FirstOrDefault()`
```csharp
var customerWithOrders = context.Customers
    .Include(c => c.Orders)
    .FirstOrDefault(c => c.CustomerId == "ALFKI");
```
`Include()` بتحمّل أوردرات العميل مع بياناته، و`FirstOrDefault()` بترجع **أول عنصر يطابق الشرط**، أو `null` لو مفيش أي تطابق (بعكس `First()` اللي بترمي Exception لو مفيش نتيجة).

#### 3. Projection — `Select()`
```csharp
var employees = context.Employees
    .Select(e => new
    {
        EmployeeName = e.FirstName + " " + e.LastName,
        OrderCount = e.Orders.Count
    })
    .ToList();
```
`Select()` بتستخدم لعمل **Projection** — يعني بدل ما تجيب كل بيانات الموظف، بتختار بس الحقول اللي محتاجها (وممكن كمان تعمل حسابات زي `e.Orders.Count`) وترجعهم في شكل جديد (Anonymous Type هنا).

#### 4. Sorting — `OrderBy()`
```csharp
var customers = context.Customers
    .OrderBy(c => c.CompanyName)
    .ToList();
```
`OrderBy()` بترتب النتائج تصاعديًا حسب القيمة اللي تحددها.

---

## 🔄 Code First مقابل Database First

| Code First | Database First |
|---|---|
| بتعمل الـ Models الأول | قاعدة البيانات موجودة بالفعل |
| قاعدة البيانات بتتولد من الـ Models | الـ Models بتتولد من قاعدة البيانات |
| بيستخدم EF Core Migrations | بيستخدم Reverse Engineering / Scaffolding |
| `Add-Migration` | EF Core Power Tools |
| `Update-Database` | استخدام قاعدة بيانات موجودة |
| `LibraryDb` | `Northwind` |

---

## ▶️ إزاي تشغل الـ Solution

### Part 1
1. افتح الـ Solution في Visual Studio.
2. تأكد من الـ Connection String في `LibraryContext`.
3. من Package Manager Console نفّذ `Update-Database`.
4. شغّل المشروع.

### Part 2
1. تأكد إن قاعدة بيانات **Northwind** موجودة على SQL Server عندك.
2. افتح مشروع الـ Database First.
3. تأكد إن الـ `DbContext` بيتصل بالسيرفر الصح.
4. شغّل المشروع، واستعلامات LINQ هتجيب وتعرض البيانات المطلوبة.

---

## ✅ خلاصة

| المفهوم | مين استخدمه |
|---|---|
| Entity Models + Navigation Properties | Part 1 |
| DbContext & DbSet | الاتنين |
| EF Core Migrations (`Add-Migration`, `Update-Database`) | Part 1 |
| Reverse Engineering / Scaffolding (EF Core Power Tools) | Part 2 |
| `Where()` — Filtering | Part 2 |
| `Include()` + `FirstOrDefault()` — Eager Loading | Part 1 & 2 |
| `Select()` — Projection | Part 2 |
| `OrderBy()` — Sorting | Part 2 |
| `ToList()` — Deferred Execution | الاتنين |

---

## ✍️ الاسم

**Asmaa Mostafa**
تدريب ITI