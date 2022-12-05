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

namespace UserModule_BARCLAYSVIDEOWALLCONTROLFORC_
{
    public class UserModuleClass_BARCLAYSVIDEOWALLCONTROLFORC_ : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        Crestron.Logos.SplusObjects.StringInput MAIN_PROGRAM_RX__DOLLAR__;
        InOutArray<Crestron.Logos.SplusObjects.AnalogInput> INPUTFOROUTPUT;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> OUTPUT1INPUT;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> OUTPUT2INPUT;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> OUTPUT3INPUT;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> OUTPUT4INPUT;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> OUTPUT5INPUT;
        Crestron.Logos.SplusObjects.StringOutput MAIN_PROGRAM_TX__DOLLAR__;
        ushort X = 0;
        ushort I = 0;
        CrestronString INPUT__DOLLAR__;
        CrestronString OUTPUT__DOLLAR__;
        private void CLEAROUTPUT (  SplusExecutionContext __context__, ushort OUTPUT ) 
            { 
            
            __context__.SourceCodeLine = 18;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (OUTPUT == 1))  ) ) 
                { 
                __context__.SourceCodeLine = 20;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)9; 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 22;
                    OUTPUT1INPUT [ I]  .Value = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 20;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 25;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (OUTPUT == 2))  ) ) 
                { 
                __context__.SourceCodeLine = 27;
                ushort __FN_FORSTART_VAL__2 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__2 = (ushort)9; 
                int __FN_FORSTEP_VAL__2 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__2; (__FN_FORSTEP_VAL__2 > 0)  ? ( (I  >= __FN_FORSTART_VAL__2) && (I  <= __FN_FOREND_VAL__2) ) : ( (I  <= __FN_FORSTART_VAL__2) && (I  >= __FN_FOREND_VAL__2) ) ; I  += (ushort)__FN_FORSTEP_VAL__2) 
                    { 
                    __context__.SourceCodeLine = 29;
                    OUTPUT2INPUT [ I]  .Value = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 27;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 32;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (OUTPUT == 3))  ) ) 
                { 
                __context__.SourceCodeLine = 34;
                ushort __FN_FORSTART_VAL__3 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__3 = (ushort)9; 
                int __FN_FORSTEP_VAL__3 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__3; (__FN_FORSTEP_VAL__3 > 0)  ? ( (I  >= __FN_FORSTART_VAL__3) && (I  <= __FN_FOREND_VAL__3) ) : ( (I  <= __FN_FORSTART_VAL__3) && (I  >= __FN_FOREND_VAL__3) ) ; I  += (ushort)__FN_FORSTEP_VAL__3) 
                    { 
                    __context__.SourceCodeLine = 36;
                    OUTPUT3INPUT [ I]  .Value = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 34;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 39;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (OUTPUT == 4))  ) ) 
                { 
                __context__.SourceCodeLine = 41;
                ushort __FN_FORSTART_VAL__4 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__4 = (ushort)9; 
                int __FN_FORSTEP_VAL__4 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__4; (__FN_FORSTEP_VAL__4 > 0)  ? ( (I  >= __FN_FORSTART_VAL__4) && (I  <= __FN_FOREND_VAL__4) ) : ( (I  <= __FN_FORSTART_VAL__4) && (I  >= __FN_FOREND_VAL__4) ) ; I  += (ushort)__FN_FORSTEP_VAL__4) 
                    { 
                    __context__.SourceCodeLine = 43;
                    OUTPUT4INPUT [ I]  .Value = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 41;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 46;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (OUTPUT == 5))  ) ) 
                { 
                __context__.SourceCodeLine = 48;
                ushort __FN_FORSTART_VAL__5 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__5 = (ushort)9; 
                int __FN_FORSTEP_VAL__5 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__5; (__FN_FORSTEP_VAL__5 > 0)  ? ( (I  >= __FN_FORSTART_VAL__5) && (I  <= __FN_FOREND_VAL__5) ) : ( (I  <= __FN_FORSTART_VAL__5) && (I  >= __FN_FOREND_VAL__5) ) ; I  += (ushort)__FN_FORSTEP_VAL__5) 
                    { 
                    __context__.SourceCodeLine = 50;
                    OUTPUT5INPUT [ I]  .Value = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 48;
                    } 
                
                } 
            
            
            }
            
        object INPUTFOROUTPUT_OnChange_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 57;
                X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
                __context__.SourceCodeLine = 59;
                INPUT__DOLLAR__  .UpdateValue ( Functions.ItoA (  (int) ( INPUTFOROUTPUT[ X ] .UshortValue ) )  ) ; 
                __context__.SourceCodeLine = 60;
                OUTPUT__DOLLAR__  .UpdateValue ( Functions.ItoA (  (int) ( X ) )  ) ; 
                __context__.SourceCodeLine = 62;
                MAIN_PROGRAM_TX__DOLLAR__  .UpdateValue ( "VideoMatrix:ChangeInput:" + INPUT__DOLLAR__ + ":" + OUTPUT__DOLLAR__  ) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object MAIN_PROGRAM_RX__DOLLAR___OnChange_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 67;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Left( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 5 ) ) == "Video"))  ) ) 
                { 
                __context__.SourceCodeLine = 69;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 13 ) , (int)( 11 ) ) == "ChangeInput"))  ) ) 
                    { 
                    __context__.SourceCodeLine = 71;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 27 ) , (int)( 1 ) ) == "1"))  ) ) 
                        { 
                        __context__.SourceCodeLine = 73;
                        CLEAROUTPUT (  __context__ , (ushort)( Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 27 ) , (int)( 1 ) ) ) )) ; 
                        __context__.SourceCodeLine = 74;
                        OUTPUT1INPUT [ Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 25 ) , (int)( 1 ) ) )]  .Value = (ushort) ( 1 ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 76;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 27 ) , (int)( 1 ) ) == "2"))  ) ) 
                        { 
                        __context__.SourceCodeLine = 78;
                        CLEAROUTPUT (  __context__ , (ushort)( Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 27 ) , (int)( 1 ) ) ) )) ; 
                        __context__.SourceCodeLine = 79;
                        OUTPUT2INPUT [ Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 25 ) , (int)( 1 ) ) )]  .Value = (ushort) ( 1 ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 81;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 27 ) , (int)( 1 ) ) == "3"))  ) ) 
                        { 
                        __context__.SourceCodeLine = 83;
                        CLEAROUTPUT (  __context__ , (ushort)( Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 27 ) , (int)( 1 ) ) ) )) ; 
                        __context__.SourceCodeLine = 84;
                        OUTPUT3INPUT [ Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 25 ) , (int)( 1 ) ) )]  .Value = (ushort) ( 1 ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 86;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 27 ) , (int)( 1 ) ) == "4"))  ) ) 
                        { 
                        __context__.SourceCodeLine = 88;
                        CLEAROUTPUT (  __context__ , (ushort)( Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 27 ) , (int)( 1 ) ) ) )) ; 
                        __context__.SourceCodeLine = 89;
                        OUTPUT4INPUT [ Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 25 ) , (int)( 1 ) ) )]  .Value = (ushort) ( 1 ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 91;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 27 ) , (int)( 1 ) ) == "5"))  ) ) 
                        { 
                        __context__.SourceCodeLine = 93;
                        CLEAROUTPUT (  __context__ , (ushort)( Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 27 ) , (int)( 1 ) ) ) )) ; 
                        __context__.SourceCodeLine = 94;
                        OUTPUT5INPUT [ Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 25 ) , (int)( 1 ) ) )]  .Value = (ushort) ( 1 ) ; 
                        } 
                    
                    } 
                
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
    INPUT__DOLLAR__  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, this );
    OUTPUT__DOLLAR__  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, this );
    
    OUTPUT1INPUT = new InOutArray<DigitalOutput>( 9, this );
    for( uint i = 0; i < 9; i++ )
    {
        OUTPUT1INPUT[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( OUTPUT1INPUT__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( OUTPUT1INPUT__DigitalOutput__ + i, OUTPUT1INPUT[i+1] );
    }
    
    OUTPUT2INPUT = new InOutArray<DigitalOutput>( 9, this );
    for( uint i = 0; i < 9; i++ )
    {
        OUTPUT2INPUT[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( OUTPUT2INPUT__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( OUTPUT2INPUT__DigitalOutput__ + i, OUTPUT2INPUT[i+1] );
    }
    
    OUTPUT3INPUT = new InOutArray<DigitalOutput>( 9, this );
    for( uint i = 0; i < 9; i++ )
    {
        OUTPUT3INPUT[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( OUTPUT3INPUT__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( OUTPUT3INPUT__DigitalOutput__ + i, OUTPUT3INPUT[i+1] );
    }
    
    OUTPUT4INPUT = new InOutArray<DigitalOutput>( 9, this );
    for( uint i = 0; i < 9; i++ )
    {
        OUTPUT4INPUT[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( OUTPUT4INPUT__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( OUTPUT4INPUT__DigitalOutput__ + i, OUTPUT4INPUT[i+1] );
    }
    
    OUTPUT5INPUT = new InOutArray<DigitalOutput>( 9, this );
    for( uint i = 0; i < 9; i++ )
    {
        OUTPUT5INPUT[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( OUTPUT5INPUT__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( OUTPUT5INPUT__DigitalOutput__ + i, OUTPUT5INPUT[i+1] );
    }
    
    INPUTFOROUTPUT = new InOutArray<AnalogInput>( 5, this );
    for( uint i = 0; i < 5; i++ )
    {
        INPUTFOROUTPUT[i+1] = new Crestron.Logos.SplusObjects.AnalogInput( INPUTFOROUTPUT__AnalogSerialInput__ + i, INPUTFOROUTPUT__AnalogSerialInput__, this );
        m_AnalogInputList.Add( INPUTFOROUTPUT__AnalogSerialInput__ + i, INPUTFOROUTPUT[i+1] );
    }
    
    MAIN_PROGRAM_RX__DOLLAR__ = new Crestron.Logos.SplusObjects.StringInput( MAIN_PROGRAM_RX__DOLLAR____AnalogSerialInput__, 1000, this );
    m_StringInputList.Add( MAIN_PROGRAM_RX__DOLLAR____AnalogSerialInput__, MAIN_PROGRAM_RX__DOLLAR__ );
    
    MAIN_PROGRAM_TX__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( MAIN_PROGRAM_TX__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( MAIN_PROGRAM_TX__DOLLAR____AnalogSerialOutput__, MAIN_PROGRAM_TX__DOLLAR__ );
    
    
    for( uint i = 0; i < 5; i++ )
        INPUTFOROUTPUT[i+1].OnAnalogChange.Add( new InputChangeHandlerWrapper( INPUTFOROUTPUT_OnChange_0, false ) );
        
    MAIN_PROGRAM_RX__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( MAIN_PROGRAM_RX__DOLLAR___OnChange_1, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_BARCLAYSVIDEOWALLCONTROLFORC_ ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint MAIN_PROGRAM_RX__DOLLAR____AnalogSerialInput__ = 0;
const uint INPUTFOROUTPUT__AnalogSerialInput__ = 1;
const uint OUTPUT1INPUT__DigitalOutput__ = 0;
const uint OUTPUT2INPUT__DigitalOutput__ = 9;
const uint OUTPUT3INPUT__DigitalOutput__ = 18;
const uint OUTPUT4INPUT__DigitalOutput__ = 27;
const uint OUTPUT5INPUT__DigitalOutput__ = 36;
const uint MAIN_PROGRAM_TX__DOLLAR____AnalogSerialOutput__ = 0;

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
