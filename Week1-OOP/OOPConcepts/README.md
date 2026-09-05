# 📖 Book Class — OOP Concepts

مشروع بسيط بيشرح أساسيات الـ **Object-Oriented Programming (OOP)** في #C من خلال كلاس `Book` بيمثل كتاب في مكتبة.

---

## 🧩 الكود

```csharp
public class Book
{
    public string Title;
    public string Author;
    public double Price;
    public int Copies;

    public void Display() { ... }
    public void Sell(int n) { ... }
    public void Restock(int n) { ... }
    public double TotalValue() { ... }
}
```

---

## 🔑 المفاهيم

### 1. Class (الكلاس)

الـ **Class** هو الـ **blueprint أو القالب** اللي بنحدد فيه شكل وسلوك الـObjects.

هنا `Book` هو القالب اللي بيحدد البيانات والسلوك الخاص بأي كتاب.

---

### 2. Object (الكائن)

```csharp
Book b = new Book();
```

ده **Object** أو Instance من الـ`Book` Class.

لو الـClass هو القالب، فالـObject هو النسخة الفعلية اللي بنستخدمها في البرنامج.

وممكن نعمل أكتر من Object من نفس الـClass، وكل Object بيكون له بياناته الخاصة.

---

### 3. Fields (الحقول)

```csharp
public string Title;
public string Author;
public double Price;
public int Copies;
```

دي الـ **Fields** الموجودة داخل الـClass، وبتخزن بيانات الـBook.

الـFields بتمثل **حالة الكائن (State)**، وكل Object بيكون له نسخة خاصة من البيانات دي.

---

### 4. Methods (الدوال)

```csharp
public void Display()
public void Sell(int n)
public void Restock(int n)
public double TotalValue()
```

الـ **Methods** بتمثل **سلوك الكائن (Behavior)**، يعني الأفعال اللي الـBook Object يقدر يعملها.

* `Display()` → عرض بيانات الكتاب.
* `Sell()` → تقليل عدد النسخ.
* `Restock()` → زيادة عدد النسخ.
* `TotalValue()` → حساب القيمة الإجمالية للنسخ.

---

### 5. استدعاء Method جوه Method تانية

داخل `Display()`:

```csharp
Console.WriteLine($"Total Value: {TotalValue()}");
```

هنا `Display()` بتستدعي `TotalValue()` بدل ما نكرر عملية الحساب.

وده بيساعد على **تقليل تكرار الكود** وبيخلي الكود أسهل في القراءة والصيانة.

---

### 6. Parameters (الـParameters)

في:

```csharp
public void Sell(int n)
{
    Copies -= n;
}
```

الـ`n` هو **Parameter** بنستخدمه عشان نحدد عدد النسخ اللي هنبيعها.

ونفس الفكرة في:

```csharp
public void Restock(int n)
{
    Copies += n;
}
```

---

### 7. Return Value

الـ`TotalValue()` بترجع قيمة باستخدام `return`:

```csharp
public double TotalValue()
{
    return Price * Copies;
}
```

نوع القيمة اللي بترجع هو `double`.

---

### 8. Encapsulation (التغليف) — فرصة للتحسين

الـFields هنا معمولة `public`، وده معناه إن أي كود خارج الـClass يقدر يعدل عليها مباشرة.

مثلًا:

```csharp
b.Price = -50;
```

الكود هيشتغل، لكن القيمة منطقيًا غلط.

لذلك ممكن تحسين التصميم باستخدام **private fields + Properties** للتحكم في الوصول للبيانات والتحقق من القيم قبل تعديلها.

> في النسخة الحالية، الـEncapsulation **غير مطبقة بشكل كامل**.

---

## ⚙️ مثال استخدام

```csharp
Book b = new Book();

b.Title = "Clean Code";
b.Author = "Robert Martin";
b.Price = 150;
b.Copies = 10;

b.Display();

b.Sell(3);
b.Restock(5);
```

هنا أنشأنا Object من `Book`، وحددنا بياناته، وبعد كده استخدمنا الـMethods للتعامل مع حالته.

---

## ✅ خلاصة

| المفهوم             | موجود في الكود |
| ------------------- | -------------- |
| Class               | ✔️             |
| Object              | ✔️             |
| Fields              | ✔️             |
| State               | ✔️             |
| Methods             | ✔️             |
| Behavior            | ✔️             |
| Parameters          | ✔️             |
| Return Value        | ✔️             |
| Method داخل Method  | ✔️             |
| Encapsulation كاملة | ❌ فرصة للتحسين |

---

## 🎯 الهدف من التمرين

الهدف من التمرين هو فهم العلاقة بين:

**Class → Object → State + Behavior**

وكيف نقدر نجمع البيانات والوظائف المرتبطة بيها داخل Class واحدة باستخدام أساسيات الـOOP في C#.
