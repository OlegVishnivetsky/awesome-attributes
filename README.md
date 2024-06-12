<h1 align="center"><img align="center" src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/cf36e245-6acd-4da5-8070-e85b1a16f74b" width="100" height="100"> AWESOME ATTRIBUTES</h1>

**Last Updated:** 04/29/2024 

**Publisher: cherrydev** 

<aside>
💡 About: Unity asset designed to speed up and improve your development process. 
  Adds a large number of attributes with which you can make your inspector more beautiful and convenient.
</aside>

# 📥 Installation

### 1️⃣ Unity asset store (not yet available)

1. Directly from Unity asset store. Link soon 

### 2️⃣ Using GitHub link

1. Go to ‘Package Manager’ - + - ‘Add package from git URL’ and paste [https://github.com/OlegVishnivetsky/awesome-attributes.git?path=/Assets/AwesomeAttributes](https://github.com/OlegVishnivetsky/awesome-attributes.git?path=/Assets/AwesomeAttributes)

### 3️⃣ Add from disk

1. Download .zip from git hub page and extract forder.
2. Go to ‘Package Manager’ - + - ‘Add package from disk’  and select package.json file.

---
# 📝 Documentation

## 1. Title

Draws a title and subtitle (optional). You can change the **text alignment** to Left/Center/Right. You can choose whether this text will be **bold**, have a **separation line** or not.

```csharp
    [Title("Health", "Player health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    --------------------------------------------------

    public TitleAttribute(string title, string subTitle = null, bool bold = true, bool withSeparationLine = true);
    public TitleAttribute(string title, TitleTextAlignments textAlignments, string subTitle = null,  bool bold = true, bool withSeparationLine = true)
```
<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/db28898a-d926-44f5-9b2a-d21042547a81">
<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/789cd02f-a969-4908-bebc-b620763d9976">

## 2. GUI Color

Everything is simple here. The attribute changes gui color. You can use it by specifying **color hex** or **rgba** in the parameters.

```csharp
    [GUIColor("#ff00ff")]
    [SerializeField] private float maxHealth;
    [GUIColor(255, 0, 0, 0.2f)]
    [SerializeField] private float currentHealth;

    --------------------------------------------------

    public GUIColorAttribute(int r, int g, int b, float a);
    public GUIColorAttribute(string colorHex)
```
<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/aa6172c5-5c04-4916-bd93-6a2916378d7d">

## 3. Separation Line

Draws a separation line with **height, top spacing** and **bottom spacing.**

```csharp
    [SeparationLine(10)]

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [SeparationLine(1, 10, 10)]

    [SerializeField] private float speed;
    [SerializeField] public float maxSpeed = 4;

    --------------------------------------------------

    public SeparationLineAttribute(float height, float topSpacing = 1, float bottomSpacing = 1);
```
<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/e43cd314-ed3a-4923-9599-eba00bb6eab2">

## 4. Label

Changes the field name in the inspector, useful for long names. 

```csharp
    [Label("Short Name")]
    [SerializeField] private float veryveryveryveryveryLong;

    --------------------------------------------------

    public LabelAttribute(string lable)
```
<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/02c5de50-a68c-4c86-9136-f5e67175b7c9">

## 5. ShowIf

Shows the field in the inspector if the condition is true, otherwise hides it. May contain several conditions and enum. You can also specify a method that returns a bool.

```csharp
    [SerializeField] private bool showIfThisTrue;
    [ShowIf("showIfThisTrue")]
    [SerializeField] private int showMePlease;

    [SerializeField] private ShowIfTestEnum showIfEnumTest;
    [ShowIf(ShowIfTestEnum.Show, "showIfEnumTest")]
    [SerializeField] private int showEnumTest;

    --------------------------------------------------

    public ShowIfAttribute(string condition)
    public ShowIfAttribute(string conditionsOperator, params string[] conditions)
    public ShowIfAttribute(object enumValue, string enumFieldName)

```
<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/0f8b3b7d-13b1-40ef-99a1-7d5cc8294efb">

// TODO: Write documentation for the remaining attributes.

---

⭐⭐⭐⭐⭐ If you want to add your attribute. Then please follow the folder structure as in the asset and make a pull request. Feel free to edit any code to suit your needs. If you find any bugs or have any questions, you can write about it to me by email, github or in reviews in the Unity Asset Store. I will also be pleased if you visit my itchio page.  😄

Gmail: olegmroleg@gmail.com

Github: [https://github.com/OlegVishnivetsky](https://github.com/OlegVishnivetsky)

Itch.io: [https://oleg-vishnivetsky.itch.io/](https://oleg-vishnivetsky.itch.io/)

This file will be updated over time. If you write suggestions again.
