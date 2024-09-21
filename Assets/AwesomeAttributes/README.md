<h1 align="center"><img align="center" src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/cf36e245-6acd-4da5-8070-e85b1a16f74b" width="100" height="100"> AWESOME ATTRIBUTES</h1>

**Last Updated:** 08/02/2024 

**Publisher: cherrydev** 

<aside>
💡 About: Unity asset designed to speed up and improve your development process. 
  Adds a large number of attributes with which you can make your inspector more beautiful and convenient.
</aside>
</br>
</br>
Overview Video: https://www.youtube.com/watch?v=FhUs-C4zX_s&t=14s&ab_channel=cherrydev(url)

# 📥 Installation

### 1️⃣ Unity asset store (not yet available)

1. Directly from Unity asset store. Link soon 

### 2️⃣ Using GitHub link

1. Go to ‘Package Manager’ - + - ‘Add package from git URL’ and paste [https://github.com/OlegVishnivetsky/awesome-attributes.git?path=/Assets/AwesomeAttributes](https://github.com/OlegVishnivetsky/awesome-attributes.git?path=/Assets/AwesomeAttributes)

### 3️⃣ Add from disk

1. Download .zip from git hub page and extract forder.
2. Go to ‘Package Manager’ - + - ‘Add package from disk’  and select package.json file.

### 4️⃣ OpenUPM

Also this package available on [OpenUPM](https://openupm.com/packages/com.cherrydev.awesomeattributes/). You can install it using [openupm-cli](https://github.com/openupm/openupm-cli).
Install via command-line interface
```
  openupm add com.cherrydev.awesomeattributes
```
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
<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/1fbc4e26-6593-4830-86a9-2ad01cbb2e58">

## 6.  Readonly

Attribute class for **readonly** fields, they are visible in the inspector but cannot be edited.

```csharp
    [SerializeField] private float maxHealth;
    [Readonly]
    [SerializeField] private float currentHealth;
```

<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/f3147839-124e-4259-958a-056b40621cae">

## 7.  ReadonlyIf

Another **conditional** attribute. Makes the field **readonly** if the condition is true. May contain several conditions and **enum**.

```csharp
    [SerializeField] private bool turnOnReadonly;
    [ReadonlyIf("turnOnReadonly")]
    [SerializeField] private float currentHealth;

    --------------------------------------------------

    public ReadonlyIfAttribute(string condition)
    public ReadonlyIfAttribute(string conditionsOperator, params string[] conditions)
    public ReadonlyIfAttribute(object enumValue, string enumFieldName)
```

<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/d64e2ecf-4738-42e6-b12b-6bd16f41f767">

## 8.  MinMaxSlider

Attribute that creates special **slider** the user can use to specify a range between a **min** and a **max**. Can be used on Vector2 and float fields.

```csharp
    [MinMaxSlider(0, 20)]
    [SerializeField] private Vector2 minMaxValue;

    --------------------------------------------------

    public MinMaxSliderAttribute(float minValue, float maxValue)
```

<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/44c70627-1b27-4307-9569-1a723aa12863">

## 9.  WithoutLabel

Hides the field label

```csharp
    [WithoutLabel]
    [SerializeField] private Vector2 iDontNeedLabel;
```

<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/6dfd0086-fa4e-4d8c-8181-3036ab649882">

## 10.  Button

Shows a **button** under the field to which you placed the attribute. The name of the method is specified as a parameter; you can also specify the **label** and **height**.

```csharp
    [Button("DebugCurrentHealth", "Check Health")]
    [SerializeField] private float currentHealth;

    --------------------------------------------------

    public ButtonAttribute(string methodName, string lable = null, float height = 18)
```

<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/0930c652-c00f-47fa-86dc-eb4951c76a6a">

## 11.  Required

Attribute that creates a warning box if the field is null.

```csharp
    [Required]
    [SerializeField] private GameObject requiredObject;

    --------------------------------------------------

    public RequiredAttribute()
    public RequiredAttribute(string message)
    public RequiredAttribute(MessageType messageType)
    public RequiredAttribute(string message, MessageType messageType)
```

<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/dc93abbe-d846-4041-bbb8-cf3488747511">

## 12.  OnlyChildGameObjects

Restricts a property to reference only child objects of the same type. Adds a button "Pick" that opens a window with all child objects of the same type as the field and allows you to assign only child objects.

```csharp
    [OnlyChildGameObjects]
    [SerializeField] private Rigidbody2D onlyChildObjects;
```

<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/4eedc2a1-3383-40c3-88ac-a9fd1c1e9b04">

## 13.  TagSelector

Allows you to select a tag from a dropdown in the Inspector.

```csharp
    [TagSelector]
    [SerializeField] private string playerTag;
```

<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/bf27bef6-8887-4464-96d9-28f8eebf9771">

## 14.  Scene

Allows you to select a scene from the drop-down list in the Inspector for string or integer fields. The drop-down list shows the scenes that are in Build Settings/Scenes In Build

```csharp
    [Scene]
    [SerializeField] private string sceneField;
```

<img src="https://github.com/OlegVishnivetsky/awesome-attributes/assets/98222611/3ee7ef70-7067-4299-b18d-11b9687e01bf">

## 15.  PlayerPrefs

All fields marked with this attribute will be automatically saved in the OnDestroy or OnDisable methods. For this attribute to work, you need to add PlayerPrefsAttributeObserver prefab to the scene. 
As a parameter, the attribute requires a key and the type when it will be saved (optional, by default everything is saved in OnDisable()).

```csharp
    [PlayerPrefs("SaveMe")]
    [SerializeField] private int saveMe;
```

## 16.  ResourcesPath

Allows selecting assets from the Resources folder and stores the path for Resources.Load. Also restricts selection to assets within the Resources folder.

```csharp
    [ResourcesPath]
    [SerializeField] private string path;
```

## 17.  Gradient

Gradient attribute that allows you editing Gradient fields directly in the Inspector.

```csharp
    [Gradient]
    [SerializeField] private Gradient gradient;
```

## 17.  Preview

Preview attribute that can be used for sprite fields. Draws a foldout that can be toggled to see a preview of the sprite.

```csharp
    [Preview]
    [SerializeField] private Sprite testSprite;
```
<img src="https://github.com/user-attachments/assets/3b09c22f-07f6-475f-b2fa-06793a7e90f4">

<h1></h1>

⭐⭐⭐⭐⭐ If you want to add your attribute. Then please follow the folder structure as in the asset and make a pull request. Feel free to edit any code to suit your needs. If you find any bugs or have any questions, you can write about it to me by email, github or in reviews in the Unity Asset Store. I will also be pleased if you visit my itchio page.  😄

Gmail: olegmroleg@gmail.com

Github: [https://github.com/OlegVishnivetsky](https://github.com/OlegVishnivetsky)

Itch.io: [https://oleg-vishnivetsky.itch.io/](https://oleg-vishnivetsky.itch.io/)

This file will be updated over time. If you write suggestions again.
