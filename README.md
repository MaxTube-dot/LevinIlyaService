# LevinIlyaService
LevinIlya.WebService - веб сервис реализующий взаимодействи с API ФССП

--Get
public IEnumerable<IEnforcementProceeding> SearchPerson(string firstName, string lastName, string birthDate,int region , string secoundName = null)
  
--Post
SearchGroup(IGroup group)

LevinFsspParser.BL - библиотека для взаимодействия с API ФССП

--//Инициализация с токеном
FsspManager _fssp = new FsspManager("CyTa98qdpZKx");

--Единичный запрос    
//Отправляем данные 
IEnumerable<IEnforcementProceeding> enforcementProceedings = _fssp.SearchPerson( firstName,lastName, irthDate,region,secoundName);

// enforcementProceedings - список исполнительных производств

--Групповой запрос
//Создание группы
IGroup group = new GroupModel();

//Добавление физических лиц в запрос
group.AddPerson(GenerateRandomPerson());

//Получение результатов
var results = _fssp.SearchGroup(group);
