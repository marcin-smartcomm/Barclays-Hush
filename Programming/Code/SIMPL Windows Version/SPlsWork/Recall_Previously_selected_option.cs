using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;

namespace UserModule_RECALL_PREVIOUSLY_SELECTED_OPTION
{
    public class UserModuleClass_RECALL_PREVIOUSLY_SELECTED_OPTION : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        UShortParameter DEFAULT_INPUT;
        Crestron.Logos.SplusObjects.DigitalInput RECALL;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> SAVEOPTION;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> RECALLOPTION;
        object SAVEOPTION_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 15;
                _SplusNVRAM.X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
                __context__.SourceCodeLine = 16;
                _SplusNVRAM.PREVIOUSLYSELECTED = (ushort) ( _SplusNVRAM.X ) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object RECALL_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 21;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.PREVIOUSLYSELECTED == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 23;
                _SplusNVRAM.PREVIOUSLYSELECTED = (ushort) ( DEFAULT_INPUT  .Value ) ; 
                } 
            
            __context__.SourceCodeLine = 26;
            Functions.Pulse ( 1, RECALLOPTION [ _SplusNVRAM.PREVIOUSLYSELECTED] ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    

public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
    _SplusNVRAM = new SplusNVRAM( this );
    
    RECALL = new Crestron.Logos.SplusObjects.DigitalInput( RECALL__DigitalInput__, this );
    m_DigitalInputList.Add( RECALL__DigitalInput__, RECALL );
    
    SAVEOPTION = new InOutArray<DigitalInput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        SAVEOPTION[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( SAVEOPTION__DigitalInput__ + i, SAVEOPTION__DigitalInput__, this );
        m_DigitalInputList.Add( SAVEOPTION__DigitalInput__ + i, SAVEOPTION[i+1] );
    }
    
    RECALLOPTION = new InOutArray<DigitalOutput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        RECALLOPTION[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( RECALLOPTION__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( RECALLOPTION__DigitalOutput__ + i, RECALLOPTION[i+1] );
    }
    
    DEFAULT_INPUT = new UShortParameter( DEFAULT_INPUT__Parameter__, this );
    m_ParameterList.Add( DEFAULT_INPUT__Parameter__, DEFAULT_INPUT );
    
    
    for( uint i = 0; i < 10; i++ )
        SAVEOPTION[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( SAVEOPTION_OnPush_0, false ) );
        
    RECALL.OnDigitalPush.Add( new InputChangeHandlerWrapper( RECALL_OnPush_1, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_RECALL_PREVIOUSLY_SELECTED_OPTION ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint DEFAULT_INPUT__Parameter__ = 10;
const uint RECALL__DigitalInput__ = 0;
const uint SAVEOPTION__DigitalInput__ = 1;
const uint RECALLOPTION__DigitalOutput__ = 0;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    [SplusStructAttribute(0, false, true)]
            public ushort X = 0;
            [SplusStructAttribute(1, false, true)]
            public ushort PREVIOUSLYSELECTED = 0;
            
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
