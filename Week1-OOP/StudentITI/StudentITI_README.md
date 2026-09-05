# 🎓 Student Class — OOP Concepts

مشروع بيشرح **Encapsulation الصحيحة** و**Validation** جوه الـ Properties، من خلال كلاس `Student` (في Solution مستقل عن باقي المشاريع).

### 🧩 الكود

```csharp
public class Student
{
    private string name;
    private double gpa;
    private string email;

    public string Name { get; set; }   // مع validation
    public double Gpa { get; set; }    // مع validation
    public string Email { get; set; }  // مع validation
}
```

### 🔑 المفاهيم

#### Encapsulation حقيقية (Private Fields + Public Properties)
```csharp
private string name;

public string Name
{
    get { return name; }
    set
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Name cannot be empty.");
        name = value.Trim();
    }
}
```
هنا الفرق عن كلاس `Book` القديم: الـ Fields بقت **private**، ومحدش يقدر يوصلها مباشرة من برة. بدل كده، فيه **Properties** بتتحكم في الوصول: `get` بيرجّع القيمة، و`set` بيتأكد إن القيمة صح قبل ما يخزنها. ده التطبيق الصح لمبدأ **Encapsulation**.

#### Validation جوه الـ Setter
```csharp
public double Gpa
{
    get { return gpa; }
    set
    {
        if (value < 0 || value > 4)
            throw new ArgumentException("GPA must be between 0 and 4.");
        gpa = value;
    }
}
```
كل `set` فيه شرط بيتأكد إن القيمة منطقية: `Name` مينفعش فاضي، `Gpa` لازم بين 0 و4، `Email` لازم فيه `@` ومن غير مسافات. لو غلط، بيترمي `ArgumentException` فورًا بدل ما يخزن بيانات فاسدة.

#### Read-only Property (Id)
```csharp
public int Id { get; }
```
بيتحدد مرة واحدة بس جوه الـ Constructor وبعدها **ثابت طول عمر الكائن** — منطقي لأن رقم الطالب مفروض ميتغيرش أبدًا.

#### Static Fields للـ Auto-Increment ID
```csharp
private static int nextId = 81001;
...
Id = nextId++;
```
`nextId` متغير **static** مشترك بين كل كائنات `Student`. كل طالب جديد بياخد الرقم الحالي ويزوّده واحد للي بعده، فكل طالب بياخد ID فريد يبدأ من 81001.

#### Computed Properties (خصائص محسوبة)
```csharp
public string Status
{
    get
    {
        if (Gpa >= 3.5) return "Excellent";
        if (Gpa >= 2.0) return "Good";
        return "At Risk";
    }
}
```
`Status` و `Initials` مالهومش Field مخزّن — بيتحسبوا **لحظيًا** كل مرة تقراهم، بناءً على قيم تانية (`Gpa`, `Name`).

#### Constructor Overloading + Chaining بـ this()
```csharp
public Student(string name, double gpa, string email)   // 1. Full
public Student(string name) : this(name, 0, "unknown@example.com")   // 2.
public Student() : this("Unknown Student", 0, "unknown@example.com") // 3.
```
3 Constructors بأشكال مختلفة (Overloading). الأقصر بينادوا على الأطول (`: this(...)`) بدل ما يكرروا نفس كود التهيئة، فكل الـ Validation والـ `Id` بتتحسب في مكان واحد بس.

#### Private Helper Method (Static)
```csharp
private static string GetInitials(string fullName)
{
    string[] parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string result = "";
    foreach (string part in parts)
        result += char.ToUpper(part[0]) + ".";
    return result;
}
```
Method مساعدة `private static` بتاخد الاسم الكامل وترجع الحروف الأولى منه (زي "Asmaa Mostafa" → "A.M."). معمولة `static` لأنها مش محتاجة أي بيانات من كائن معين.

### ✅ خلاصة القسم التالت

| المفهوم | مثال من الكود |
|---|---|
| Encapsulation (Private Fields + Public Properties) | `private string name` + `public string Name { get; set; }` |
| Validation في الـ Setter | `if (value < 0 \|\| value > 4) throw ...` |
| Read-only Property | `public int Id { get; }` |
| Static Field (Auto-Increment) | `private static int nextId` |
| Computed Property | `public string Status { get { ... } }` |
| Constructor Overloading | 3 نسخ مختلفة من `Student(...)` |
| Constructor Chaining بـ this() | `: this(name, 0, "...")` |
| Private Static Helper Method | `GetInitials(...)` |


---

## ✍️ الاسم

**Asmaa Mostafa**
تدريب ITI