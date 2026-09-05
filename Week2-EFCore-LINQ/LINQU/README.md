# 🔗 LINQ Exercises in C#

مجموعة تمارين في **LINQ** بتغطي أهم الـ Operators والمفاهيم اللي هتحتاجها وانتي بتستعلمي على أي Collection في #C.

---

## 📚 المواضيع اللي اتغطت

- Restriction Operators
- Element Operators
- Set Operators
- Aggregate Operators
- Ordering Operators
- Partitioning Operators
- Projection Operators
- Quantifiers
- Grouping Operators

---

## 📂 هيكل المشروع

```text
L2O___D09/
│
├── Program.cs
├── ListGenerators.cs
├── dictionary_english.txt
└── README.md
```

> `ListGenerators.cs` فيه بيانات جاهزة (منتجات وعملاء) بتتستخدم في كل التمارين.

---

## 🔑 المفاهيم

### 1. Restriction Operators — الفلترة
```csharp
var result = ListGenerators.ProductList
    .Where(p => p.UnitsInStock == 0);
```
`Where()` بتفلتر أي Collection بناءً على شرط، وبترجع بس العناصر اللي بتحقق الشرط ده (زي المنتجات اللي خلصت من المخزون).

### 2. Element Operators — استرجاع عنصر واحد
```csharp
var result = numbers.Where(x => x > 5).ElementAt(1);
```
- `First()` / `FirstOrDefault()`: بترجع أول عنصر يطابق شرط، والفرق إن `FirstOrDefault()` بترجع `null` أو القيمة الافتراضية لو مفيش تطابق بدل ما ترمي Exception.
- `ElementAt()`: بترجع العنصر في **مكان (Index)** معين جوه النتيجة.

### 3. Set Operators — عمليات المجموعات
```csharp
var result = productLetters.Union(customerLetters);
```
دي عمليات زي في الرياضة بالظبط:
- `Distinct()`: يشيل التكرار.
- `Union()`: دمج مجموعتين من غير تكرار.
- `Intersect()`: العناصر المشتركة بين مجموعتين.
- `Except()`: العناصر الموجودة في مجموعة ومش موجودة في التانية.
- `Concat()`: دمج بسيط (بيسمح بالتكرار).

### 4. Aggregate Operators — عمليات تجميعية
```csharp
var result = ListGenerators.ProductList
    .GroupBy(p => p.Category)
    .Select(g => new { Category = g.Key, ProductCount = g.Count() });
```
دي بتاخد Collection كاملة وترجع **قيمة واحدة** تلخصها: `Count()`, `Sum()`, `Min()`, `Max()`, `Average()`. غالبًا بتتستخدم بعد `GroupBy()` عشان تحسب إحصائية لكل مجموعة (زي عدد المنتجات في كل تصنيف).

### 5. Ordering Operators — الترتيب
```csharp
var result = ListGenerators.ProductList
    .OrderBy(p => p.Category)
    .ThenByDescending(p => p.UnitPrice);
```
`OrderBy()` بترتب تصاعديًا، و`OrderByDescending()` تنازليًا. `ThenBy()` / `ThenByDescending()` بتضيف **مستوى ترتيب تاني** لما القيم الأولى تتساوى (هنا: رتّب حسب الفئة، وجوه كل فئة رتّب حسب السعر تنازليًا).

### 6. Partitioning Operators — تقسيم النتيجة
```csharp
var result = numbers.TakeWhile((x, index) => x >= index);
```
- `Take(n)` / `Skip(n)`: خد أو تخطى أول عدد عناصر معين.
- `TakeWhile()` / `SkipWhile()`: خد أو تخطى العناصر **طول ما الشرط صح**، ووقف أول ما الشرط يفشل (بعكس `Where()` اللي بيفحص كل عنصر لوحده). لاحظ هنا استخدام الـ **Index** جوه الشرط نفسه.

### 7. Projection Operators — إعادة تشكيل البيانات
```csharp
var result = ListGenerators.ProductList
    .Select(p => new { p.ProductName, p.Category, Price = p.UnitPrice });
```
- `Select()`: بتحوّل كل عنصر لشكل جديد (هنا Anonymous Type فيه 3 خصائص بس بدل الكائن الكامل).
- `SelectMany()`: بتستخدم لما يكون عندك **Collection جوه Collection** (زي أوردرات جوه كل عميل) وعايزة "تفرد" كل حاجة في مستوى واحد بدل Nested Loops.

### 8. Quantifiers — أسئلة صح/غلط على المجموعة كلها
```csharp
var result = words.Any(w => w.Contains("ei"));
```
- `Any()`: هل **يوجد عنصر واحد على الأقل** يحقق الشرط؟
- `All()`: هل **كل العناصر** بتحقق الشرط؟

### 9. Grouping Operators — التجميع
```csharp
var result = groupingNumbers.GroupBy(x => x % 5);
```
`GroupBy()` بتقسم الـ Collection لمجموعات بناءً على **مفتاح (Key)** بتحدده انتي — هنا كل رقم بيتحط في مجموعة حسب باقي قسمته على 5. كل مجموعة ليها `Key` (المفتاح) ومحتوياتها.

---

## 🧩 Custom Anagram Comparer

جزء إضافي في المشروع بيعمل **Comparer مخصص** عشان يجمع الكلمات اللي بتتكون من نفس الحروف (Anagrams) زي `from` و `form`:

```csharp
class AnagramComparer : IEqualityComparer<string>
{
    public bool Equals(string? x, string? y)
    {
        if (x == null || y == null)
            return x == y;

        return string.Concat(x.OrderBy(c => c))
            .Equals(string.Concat(y.OrderBy(c => c)));
    }

    public int GetHashCode(string obj)
    {
        return string.Concat(obj.OrderBy(c => c)).GetHashCode();
    }
}
```
الفكرة: أي كلمتين لو رتّبنا حروفهم أبجديًا وبقوا متطابقين، معناها إنهم Anagram لبعض. الكلاس ده بيطبق **`IEqualityComparer<string>`** — إنترفيس بيدّي شكل مخصص لـ "المساواة" بين عنصرين، عشان تقدري تستخدميه جوه `GroupBy()` أو `Distinct()` بمنطق مختلف عن المقارنة الافتراضية.

---

## 🚀 إزاي تشغلي المشروع

1. اعملي Clone للريبو أو افتحي المجلد في Visual Studio.
2. تأكدي إن الملفات دي موجودة: `Program.cs`, `ListGenerators.cs`, `dictionary_english.txt`.
3. تأكدي إن `dictionary_english.txt` موجود في نفس مسار التشغيل (Runtime Path).
4. شغلي المشروع — لو كل حاجة مظبوطة هيظهر في الآخر:
```text
LINQ Questions Completed Successfully!
```

---

## ✅ خلاصة

| الفئة | أمثلة على الـ Operators |
|---|---|
| Restriction | `Where()` |
| Element | `First()`, `FirstOrDefault()`, `ElementAt()` |
| Set | `Distinct()`, `Union()`, `Intersect()`, `Except()`, `Concat()` |
| Aggregate | `Count()`, `Sum()`, `Min()`, `Max()`, `Average()` |
| Ordering | `OrderBy()`, `ThenBy()` |
| Partitioning | `Take()`, `Skip()`, `TakeWhile()`, `SkipWhile()` |
| Projection | `Select()`, `SelectMany()` |
| Quantifiers | `Any()`, `All()` |
| Grouping | `GroupBy()` |
| Custom Comparer | `IEqualityComparer<T>` |

---

## ✍️ الاسم

**Asmaa Mostafa**
تدريب ITI