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

namespace UserModule_BARCLAYS_DEBOUNCE
{
    public class UserModuleClass_BARCLAYS_DEBOUNCE : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        StringParameter DAYINDEX;
        Crestron.Logos.SplusObjects.DigitalInput FEEDBACK;
        Crestron.Logos.SplusObjects.DigitalInput PRESS;
        Crestron.Logos.SplusObjects.StringOutput TX__DOLLAR__;
        object PRESS_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 12;
                if ( Functions.TestForTrue  ( ( FEEDBACK  .Value)  ) ) 
                    { 
                    __context__.SourceCodeLine = 14;
                    TX__DOLLAR__  .UpdateValue ( "Scheduler:DayState:" + DAYINDEX + ":false"  ) ; 
                    __context__.SourceCodeLine = 15;
                    Functions.Delay (  (int) ( 50 ) ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 19;
                    TX__DOLLAR__  .UpdateValue ( "Scheduler:DayState:" + DAYINDEX + ":true"  ) ; 
                    __context__.SourceCodeLine = 20;
                    Functions.Delay (  (int) ( 50 ) ) ; 
                    } 
                
                
                
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
        
        FEEDBACK = new Crestron.Logos.SplusObjects.DigitalInput( FEEDBACK__DigitalInput__, this );
        m_DigitalInputList.Add( FEEDBACK__DigitalInput__, FEEDBACK );
        
        PRESS = new Crestron.Logos.SplusObjects.DigitalInput( PRESS__DigitalInput__, this );
        m_DigitalInputList.Add( PRESS__DigitalInput__, PRESS );
        
        TX__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( TX__DOLLAR____AnalogSerialOutput__, this );
        m_StringOutputList.Add( TX__DOLLAR____AnalogSerialOutput__, TX__DOLLAR__ );
        
        DAYINDEX = new StringParameter( DAYINDEX__Parameter__, this );
        m_ParameterList.Add( DAYINDEX__Parameter__, DAYINDEX );
        
        
        PRESS.OnDigitalPush.Add( new InputChangeHandlerWrapper( PRESS_OnPush_0, false ) );
        
        _SplusNVRAM.PopulateCustomAttributeList( true );
        
        NVRAM = _SplusNVRAM;
        
    }
    
    public override void LogosSimplSharpInitialize()
    {
        
        
    }
    
    public UserModuleClass_BARCLAYS_DEBOUNCE ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}
    
    
    
    
    const uint DAYINDEX__Parameter__ = 10;
    const uint FEEDBACK__DigitalInput__ = 0;
    const uint PRESS__DigitalInput__ = 1;
    const uint TX__DOLLAR____AnalogSerialOutput__ = 0;
    
    [SplusStructAttribute(-1, true, false)]
    public class SplusNVRAM : SplusStructureBase
    {
    
        public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
        
        
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
