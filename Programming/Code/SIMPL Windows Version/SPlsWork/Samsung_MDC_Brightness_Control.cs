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

namespace UserModule_SAMSUNG_MDC_BRIGHTNESS_CONTROL
{
    public class UserModuleClass_SAMSUNG_MDC_BRIGHTNESS_CONTROL : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        UShortParameter DISPLAYIDBYTE;
        UShortParameter COMMANDBYTE;
        UShortParameter DATALENGTHBYTE;
        Crestron.Logos.SplusObjects.DigitalInput CONNECTEDTOBOX;
        Crestron.Logos.SplusObjects.DigitalInput BRIGHTNESS_UP;
        Crestron.Logos.SplusObjects.DigitalInput BRIGHTNESS_DOWN;
        Crestron.Logos.SplusObjects.AnalogInput BRIGHTNESSLEVEL__POUND__;
        Crestron.Logos.SplusObjects.StringInput SAMSUNG_RX__DOLLAR__;
        Crestron.Logos.SplusObjects.AnalogOutput BRIGHTNESSLEVEL_FB__POUND__;
        Crestron.Logos.SplusObjects.StringOutput SAMSUNG_TX__DOLLAR__;
        CrestronString DISPLAYIDBYTECHR;
        CrestronString COMMANDBYTECHR;
        CrestronString DATALENGTHBYTECHR;
        ushort CURRENTBRIGHTNESS = 0;
        private void GETCURRENTBRIGHTNESS (  SplusExecutionContext __context__ ) 
            { 
            ushort SUM = 0;
            
            CrestronString CHECKSUM;
            CHECKSUM  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
            
            
            __context__.SourceCodeLine = 26;
            SUM = (ushort) ( (COMMANDBYTE  .Value + DISPLAYIDBYTE  .Value) ) ; 
            __context__.SourceCodeLine = 27;
            CHECKSUM  .UpdateValue ( Functions.Chr (  (int) ( Functions.Low( (ushort) SUM ) ) )  ) ; 
            __context__.SourceCodeLine = 29;
            SAMSUNG_TX__DOLLAR__  .UpdateValue ( "\u00AA" + COMMANDBYTECHR + DISPLAYIDBYTECHR + "\u0000" + CHECKSUM  ) ; 
            
            }
            
        private void CHANGEBRIGHTNESS (  SplusExecutionContext __context__, ushort LEVEL ) 
            { 
            CrestronString CHECKSUM;
            CHECKSUM  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
            
            CrestronString BRIGHTNESSLEVELCHR;
            BRIGHTNESSLEVELCHR  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
            
            CrestronString COMMAND;
            COMMAND  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
            
            ushort SUM = 0;
            
            
            __context__.SourceCodeLine = 39;
            BRIGHTNESSLEVELCHR  .UpdateValue ( Functions.Chr (  (int) ( LEVEL ) )  ) ; 
            __context__.SourceCodeLine = 41;
            SUM = (ushort) ( (((COMMANDBYTE  .Value + DISPLAYIDBYTE  .Value) + DATALENGTHBYTE  .Value) + LEVEL) ) ; 
            __context__.SourceCodeLine = 42;
            CHECKSUM  .UpdateValue ( Functions.Chr (  (int) ( Functions.Low( (ushort) SUM ) ) )  ) ; 
            __context__.SourceCodeLine = 44;
            COMMAND  .UpdateValue ( "\u00AA" + COMMANDBYTECHR + DISPLAYIDBYTECHR + DATALENGTHBYTECHR + BRIGHTNESSLEVELCHR + CHECKSUM  ) ; 
            __context__.SourceCodeLine = 45;
            SAMSUNG_TX__DOLLAR__  .UpdateValue ( COMMAND  ) ; 
            
            }
            
        object BRIGHTNESSLEVEL__POUND___OnChange_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 50;
                CHANGEBRIGHTNESS (  __context__ , (ushort)( BRIGHTNESSLEVEL__POUND__  .UshortValue )) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object SAMSUNG_RX__DOLLAR___OnChange_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            CrestronString TOCHECK__DOLLAR__;
            CrestronString DISPLYCHR__DOLLAR__;
            TOCHECK__DOLLAR__  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
            DISPLYCHR__DOLLAR__  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
            
            
            __context__.SourceCodeLine = 57;
            TOCHECK__DOLLAR__  .UpdateValue ( "\u00AA\u00FF" + DISPLAYIDBYTECHR + "\u0003" + "A" + COMMANDBYTECHR  ) ; 
            __context__.SourceCodeLine = 58;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Left( SAMSUNG_RX__DOLLAR__ , (int)( 6 ) ) == TOCHECK__DOLLAR__))  ) ) 
                { 
                __context__.SourceCodeLine = 60;
                CURRENTBRIGHTNESS = (ushort) ( Byte( Functions.Mid( SAMSUNG_RX__DOLLAR__ , (int)( 7 ) , (int)( 1 ) ) , (int)( 1 ) ) ) ; 
                __context__.SourceCodeLine = 61;
                BRIGHTNESSLEVEL_FB__POUND__  .Value = (ushort) ( CURRENTBRIGHTNESS ) ; 
                } 
            
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object CONNECTEDTOBOX_OnPush_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 67;
        GETCURRENTBRIGHTNESS (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object BRIGHTNESS_UP_OnPush_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        ushort NEWVALUE = 0;
        
        
        __context__.SourceCodeLine = 74;
        if ( Functions.TestForTrue  ( ( CONNECTEDTOBOX  .Value)  ) ) 
            { 
            __context__.SourceCodeLine = 76;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( CURRENTBRIGHTNESS < 95 ))  ) ) 
                { 
                __context__.SourceCodeLine = 78;
                NEWVALUE = (ushort) ( (CURRENTBRIGHTNESS + 5) ) ; 
                __context__.SourceCodeLine = 79;
                CHANGEBRIGHTNESS (  __context__ , (ushort)( NEWVALUE )) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 83;
                CHANGEBRIGHTNESS (  __context__ , (ushort)( 100 )) ; 
                } 
            
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object BRIGHTNESS_DOWN_OnPush_4 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        ushort NEWVALUE = 0;
        
        
        __context__.SourceCodeLine = 92;
        if ( Functions.TestForTrue  ( ( CONNECTEDTOBOX  .Value)  ) ) 
            { 
            __context__.SourceCodeLine = 94;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( CURRENTBRIGHTNESS > 5 ))  ) ) 
                { 
                __context__.SourceCodeLine = 96;
                NEWVALUE = (ushort) ( (CURRENTBRIGHTNESS - 5) ) ; 
                __context__.SourceCodeLine = 97;
                CHANGEBRIGHTNESS (  __context__ , (ushort)( NEWVALUE )) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 101;
                CHANGEBRIGHTNESS (  __context__ , (ushort)( 0 )) ; 
                } 
            
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

public override object FunctionMain (  object __obj__ ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 108;
        COMMANDBYTECHR  .UpdateValue ( Functions.Chr (  (int) ( COMMANDBYTE  .Value ) )  ) ; 
        __context__.SourceCodeLine = 109;
        DISPLAYIDBYTECHR  .UpdateValue ( Functions.Chr (  (int) ( DISPLAYIDBYTE  .Value ) )  ) ; 
        __context__.SourceCodeLine = 110;
        DATALENGTHBYTECHR  .UpdateValue ( Functions.Chr (  (int) ( DATALENGTHBYTE  .Value ) )  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    DISPLAYIDBYTECHR  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 2, this );
    COMMANDBYTECHR  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 2, this );
    DATALENGTHBYTECHR  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 2, this );
    
    CONNECTEDTOBOX = new Crestron.Logos.SplusObjects.DigitalInput( CONNECTEDTOBOX__DigitalInput__, this );
    m_DigitalInputList.Add( CONNECTEDTOBOX__DigitalInput__, CONNECTEDTOBOX );
    
    BRIGHTNESS_UP = new Crestron.Logos.SplusObjects.DigitalInput( BRIGHTNESS_UP__DigitalInput__, this );
    m_DigitalInputList.Add( BRIGHTNESS_UP__DigitalInput__, BRIGHTNESS_UP );
    
    BRIGHTNESS_DOWN = new Crestron.Logos.SplusObjects.DigitalInput( BRIGHTNESS_DOWN__DigitalInput__, this );
    m_DigitalInputList.Add( BRIGHTNESS_DOWN__DigitalInput__, BRIGHTNESS_DOWN );
    
    BRIGHTNESSLEVEL__POUND__ = new Crestron.Logos.SplusObjects.AnalogInput( BRIGHTNESSLEVEL__POUND____AnalogSerialInput__, this );
    m_AnalogInputList.Add( BRIGHTNESSLEVEL__POUND____AnalogSerialInput__, BRIGHTNESSLEVEL__POUND__ );
    
    BRIGHTNESSLEVEL_FB__POUND__ = new Crestron.Logos.SplusObjects.AnalogOutput( BRIGHTNESSLEVEL_FB__POUND____AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( BRIGHTNESSLEVEL_FB__POUND____AnalogSerialOutput__, BRIGHTNESSLEVEL_FB__POUND__ );
    
    SAMSUNG_RX__DOLLAR__ = new Crestron.Logos.SplusObjects.StringInput( SAMSUNG_RX__DOLLAR____AnalogSerialInput__, 100, this );
    m_StringInputList.Add( SAMSUNG_RX__DOLLAR____AnalogSerialInput__, SAMSUNG_RX__DOLLAR__ );
    
    SAMSUNG_TX__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( SAMSUNG_TX__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( SAMSUNG_TX__DOLLAR____AnalogSerialOutput__, SAMSUNG_TX__DOLLAR__ );
    
    DISPLAYIDBYTE = new UShortParameter( DISPLAYIDBYTE__Parameter__, this );
    m_ParameterList.Add( DISPLAYIDBYTE__Parameter__, DISPLAYIDBYTE );
    
    COMMANDBYTE = new UShortParameter( COMMANDBYTE__Parameter__, this );
    m_ParameterList.Add( COMMANDBYTE__Parameter__, COMMANDBYTE );
    
    DATALENGTHBYTE = new UShortParameter( DATALENGTHBYTE__Parameter__, this );
    m_ParameterList.Add( DATALENGTHBYTE__Parameter__, DATALENGTHBYTE );
    
    
    BRIGHTNESSLEVEL__POUND__.OnAnalogChange.Add( new InputChangeHandlerWrapper( BRIGHTNESSLEVEL__POUND___OnChange_0, false ) );
    SAMSUNG_RX__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( SAMSUNG_RX__DOLLAR___OnChange_1, false ) );
    CONNECTEDTOBOX.OnDigitalPush.Add( new InputChangeHandlerWrapper( CONNECTEDTOBOX_OnPush_2, false ) );
    BRIGHTNESS_UP.OnDigitalPush.Add( new InputChangeHandlerWrapper( BRIGHTNESS_UP_OnPush_3, false ) );
    BRIGHTNESS_DOWN.OnDigitalPush.Add( new InputChangeHandlerWrapper( BRIGHTNESS_DOWN_OnPush_4, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_SAMSUNG_MDC_BRIGHTNESS_CONTROL ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint DISPLAYIDBYTE__Parameter__ = 10;
const uint COMMANDBYTE__Parameter__ = 11;
const uint DATALENGTHBYTE__Parameter__ = 12;
const uint CONNECTEDTOBOX__DigitalInput__ = 0;
const uint BRIGHTNESS_UP__DigitalInput__ = 1;
const uint BRIGHTNESS_DOWN__DigitalInput__ = 2;
const uint BRIGHTNESSLEVEL__POUND____AnalogSerialInput__ = 0;
const uint SAMSUNG_RX__DOLLAR____AnalogSerialInput__ = 1;
const uint BRIGHTNESSLEVEL_FB__POUND____AnalogSerialOutput__ = 0;
const uint SAMSUNG_TX__DOLLAR____AnalogSerialOutput__ = 1;

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
