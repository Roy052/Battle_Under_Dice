# Battle_Under_Dice
<div>
    <h2> 게임 정보 </h2>
    <h4> 개발 일자 : 개발 중 <br><br>
    
  </div>
  <div>
    <h2> 게임 설명 </h2>
    <h3> 게임 플레이 </h3>
     스킬 -> 주사위 -> 선택
     
  </div> 
   <div>
       <h2> 주요 코드 </h2>
       <h4> dictionary와 Invoke 함수를 이용한 턴 개념 </h4>
    </div>
    
```csharp
 Dictionary<int, string> stepMethod = new Dictionary<int, string>{
  { 0, "TurnStart" },
  { 1, "InTurn" },
  { 2, "Check" },
  { 3, "EndTurn" },
  { 4, "BeforeStart" }
};
    
public void RunNextStep()
{
  Invoke(stepMethod[gameStatus], 0);
}

```
