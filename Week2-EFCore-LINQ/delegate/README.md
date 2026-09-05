# ✈️ Aviation Ticket Pricing Engine

## Delegates & Functional Programming في #C

مشروع بيشرح **الـ Delegates** في #C إزاي بتخليك "تبعت method كأنها قيمة عادية" لـ method تانية. المشروع بيبدأ بـ **Custom Delegate**، وبعدين **Anonymous Methods**، وبعدين **Lambda Expressions**، ويختم بالـ Delegates الجاهزة في .NET: `Action<T>`, `Predicate<T>`, `Func<T, TResult>`.

---

## 📚 إيه هو الـ Delegate؟

الـ **Delegate** ببساطة: نوع بيقدر يحمل "إشارة" (Reference) لأي method.

> يعني تقدري تبعتي method كـ Parameter لـ method تانية.

بدل ما تكتبي منطق الخصم (Discount) جوه `ProcessTickets` نفسها، تقدري تبعتيلها الـ method اللي هتحسب الخصم، وهي تنفذها هي.

```text
Method A
   ↓
بتبعت method
   ↓
Method B
   ↓
بتنفذ الـ method اللي وصلها
```

---

## 🎫 كلاس FlightTicket

```csharp
public class FlightTicket
{
    public int TicketId { get; set; }
    public string PassengerName { get; set; }
    public decimal BasePrice { get; set; }
    public DateTime FlightTime { get; set; }
    public bool IsDelayed { get; set; }
}
```
كلاس بسيط بيمثل تذكرة طيران — بياناته هي اللي كل الأمثلة الجاية هتشتغل عليها.

---

## 🔑 المفاهيم

### 1. Custom Delegate
```csharp
public delegate decimal DiscountCalculator(FlightTicket ticket);
```
ده معناه: أي method هتتحط في `DiscountCalculator` **لازم** تاخد `FlightTicket` وترجع `decimal`. يعني الـ Delegate بيوصف **شكل (Signature)** الـ method، مش تنفيذها.
```text
Input                         Output
FlightTicket  ──────────────→ decimal
```
مثال method مطابقة للشكل ده:
```csharp
public static decimal DelayedFlightDiscount(FlightTicket ticket)
{
    if (ticket.IsDelayed)
        return ticket.BasePrice * 0.80m;
    return ticket.BasePrice;
}
```

### 2. ليه محتاجين Delegate أصلاً؟
```csharp
ProcessTickets(flightTickets, DiscountRules.DelayedFlightDiscount);
```
هنا بتبعتي حاجتين: الليست، **و method الخصم نفسها**. جوه `ProcessTickets`:
```csharp
decimal finalPrice = discount(item);
```
`discount` هنا هي الـ method اللي وصلتلها، و`discount(item)` معناها فعليًا تنفيذ `DelayedFlightDiscount(item)`. الفايدة: `ProcessTickets` ماعرفتش أي حاجة عن منطق الخصم، هي بس عارفة "هتوصلني method وهنفذها".

### 3. Static Methods و Instance Methods
```csharp
// Static — مش محتاجة Object
ProcessTickets(flightTickets, DiscountRules.DelayedFlightDiscount);

// Instance — محتاجة Object الأول
DiscountRules discountRules = new DiscountRules();
ProcessTickets(flightTickets, discountRules.VipPassengerDiscount);
```
الـ Delegate بيشتغل مع النوعين طول ما الـ Signature مطابق.

### 4. Anonymous Method
```csharp
ProcessTickets(
    flightTickets,
    delegate (FlightTicket t)
    {
        return t.BasePrice * 0.90m;
    }
);
```
لما تكوني محتاجة method **مرة واحدة بس**، مش لازم تعمليها method منفصلة بالاسم — تكتبيها مباشرة "من غير اسم" (Anonymous). مفيدة لما المنطق بسيط ومش هيتكرر.

### 5. Lambda Expression
```csharp
t => t.BasePrice * 0.90m
```
طريقة أقصر لكتابة نفس الفكرة: "خدي تذكرة اسمها `t` وارجعي 90% من سعرها". مثال أعقد شوية:
```csharp
t => t.BasePrice > 1000 ? t.BasePrice - 50 : t.BasePrice
```
"لو السعر أكبر من 1000، اخصمي 50، غير كده ارجعي السعر زي ما هو."

### 6. Action<T> — "اعمل حاجة"
```csharp
Action<FlightTicket> notificationAction
```
بتاخد Parameter وترجع `void` (مفيش نتيجة). مثال:
```csharp
public static void BroadcastNotification(
    FlightTicket ticket,
    Action<FlightTicket> notificationAction)
{
    notificationAction(ticket);
}

BroadcastNotification(
    flightTickets[0],
    t => Console.WriteLine($"[System Log] Processing ticket for {t.PassengerName}.")
);
```
استخدامات شائعة: طباعة، تسجيل Logs، إشعارات — أي حاجة بتنفذ فعل من غير ما ترجع قيمة.

### 7. Predicate<T> — "هل الحاجة دي صح؟"
```csharp
Predicate<FlightTicket> condition
```
بتاخد Parameter وترجع `bool` بس. مستخدمة أساسًا في الفلترة:
```csharp
public static List<FlightTicket> FilterTickets(
    List<FlightTicket> tickets,
    Predicate<FlightTicket> condition)
{
    List<FlightTicket> result = new List<FlightTicket>();
    foreach (var ticket in tickets)
        if (condition(ticket))
            result.Add(ticket);
    return result;
}

var delayedTickets = FilterTickets(flightTickets, t => t.IsDelayed);
```

### 8. Func<T, TResult> — "هاتلي نتيجة"
```csharp
Func<FlightTicket, decimal>
```
بتاخد Input وترجع قيمة — ده بالظبط نفس فكرة الـ Custom Delegate اللي عملناه في الأول (`DiscountCalculator`)، بس جاهزة في .NET من غير ما تعرّفيها بنفسك:
```csharp
public static void ProcessTicketsWithFunc(
    List<FlightTicket> tickets,
    Func<FlightTicket, decimal> discount)
{
    foreach (var item in tickets)
    {
        decimal finalPrice = discount(item);
        Console.WriteLine($"Passenger: {item.PassengerName}, Final Price: {finalPrice}");
    }
}
```
و`Func` مرنة، ممكن ترجع أي نوع — زي `Func<FlightTicket, string>` عشان تعملي ملخص نصي للتذكرة.

### 9. الفرق بين التلاتة

| Delegate | Input | Return | الهدف |
|---|---|---|---|
| `Action<T>` | نعم | `void` | نفّذ فعل |
| `Predicate<T>` | نعم | `bool` | افحص شرط |
| `Func<T,TResult>` | نعم | نتيجة | احسبي / رجعي حاجة |

طريقة سهلة تتذكري بيها:
```text
ACTION    → "اعمل حاجة"       → void
PREDICATE → "هل صح ولا غلط؟"  → true/false
FUNC      → "هاتلي نتيجة"      → result
```

### 10. رحلة التطور الكاملة
```text
Custom Delegate
      ↓
Anonymous Method
      ↓
Lambda Expression
      ↓
Built-in Delegates
      ↓
Action / Predicate / Func
```
الفكرة الأساسية من أول المشروع لآخره واحدة: **الـ Delegate بيخليكي تبعتي "سلوك" (method) كأنه قيمة عادية**.

---

## ✅ خلاصة

| المفهوم | مثال من الكود |
|---|---|
| Custom Delegate | `public delegate decimal DiscountCalculator(...)` |
| تمرير Method كـ Parameter | `ProcessTickets(tickets, DiscountRules.X)` |
| Static vs Instance Method | `DiscountRules.X` مقابل `discountRules.Y` |
| Anonymous Method | `delegate (FlightTicket t) { ... }` |
| Lambda Expression | `t => t.BasePrice * 0.9m` |
| `Action<T>` | تنفيذ فعل من غير نتيجة |
| `Predicate<T>` | فحص شرط (`bool`) |
| `Func<T, TResult>` | حساب وإرجاع نتيجة |

---

## ✍️ الاسم

**Asmaa Mostafa**
تدريب ITI