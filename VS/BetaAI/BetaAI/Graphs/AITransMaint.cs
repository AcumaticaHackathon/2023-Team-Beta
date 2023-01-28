using System;
using PX.Data;

namespace BetaAI
{
  public class AITransMaint : PXGraph<AITransMaint>
  {

    public PXSave<AITrans> Save;
    public PXCancel<AITrans> Cancel;

    public PXFilter<AITrans> MasterView;
    public PXFilter<AITransKeyPhrase> DetailsView;  //TODO:  Need to rename this to be specific type and add all 4


  }
}