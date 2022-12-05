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

namespace UserModule_BARCLAYS_SCHEDULE_DECODER
{
    public class UserModuleClass_BARCLAYS_SCHEDULE_DECODER : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        Crestron.Logos.SplusObjects.StringInput MAIN_PROGRAM_RX__DOLLAR__;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> DAYSONFB;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> DAYSONTIMES__DOLLAR__;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> DAYSOFFTIMES__DOLLAR__;
        ushort POINTERPOS = 0;
        ushort I = 0;
        ushort ACTUALHOUR = 0;
        private void GETDAYSTATES (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 15;
            POINTERPOS = (ushort) ( (Functions.Find( "states" , MAIN_PROGRAM_RX__DOLLAR__ ) + 9) ) ; 
            __context__.SourceCodeLine = 17;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)7; 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 19;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( POINTERPOS ) , (int)( 4 ) ) == "true"))  ) ) 
                    { 
                    __context__.SourceCodeLine = 21;
                    DAYSONFB [ I]  .Value = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 22;
                    POINTERPOS = (ushort) ( (POINTERPOS + 5) ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 26;
                    DAYSONFB [ I]  .Value = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 27;
                    POINTERPOS = (ushort) ( (POINTERPOS + 6) ) ; 
                    } 
                
                __context__.SourceCodeLine = 17;
                } 
            
            
            }
            
        private void GETONTIMES (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 34;
            POINTERPOS = (ushort) ( (Functions.Find( "onTimes" , MAIN_PROGRAM_RX__DOLLAR__ ) + 11) ) ; 
            __context__.SourceCodeLine = 36;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)7; 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 38;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( POINTERPOS ) , (int)( 1 ) ) == ",") ) || Functions.TestForTrue ( Functions.BoolToInt (Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( POINTERPOS ) , (int)( 1 ) ) == "]") )) ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 40;
                    POINTERPOS = (ushort) ( (POINTERPOS - 1) ) ; 
                    __context__.SourceCodeLine = 41;
                    ACTUALHOUR = (ushort) ( (Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( POINTERPOS ) , (int)( 1 ) ) ) - 1) ) ; 
                    __context__.SourceCodeLine = 42;
                    DAYSONTIMES__DOLLAR__ [ I]  .UpdateValue ( "0" + Functions.ItoA (  (int) ( ACTUALHOUR ) ) + ":00"  ) ; 
                    __context__.SourceCodeLine = 43;
                    POINTERPOS = (ushort) ( (POINTERPOS + 3) ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 47;
                    POINTERPOS = (ushort) ( (POINTERPOS - 1) ) ; 
                    __context__.SourceCodeLine = 48;
                    ACTUALHOUR = (ushort) ( (Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( POINTERPOS ) , (int)( 2 ) ) ) - 1) ) ; 
                    __context__.SourceCodeLine = 49;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( ACTUALHOUR < 10 ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 51;
                        DAYSONTIMES__DOLLAR__ [ I]  .UpdateValue ( "0" + Functions.ItoA (  (int) ( ACTUALHOUR ) ) + ":00"  ) ; 
                        } 
                    
                    else 
                        { 
                        __context__.SourceCodeLine = 55;
                        DAYSONTIMES__DOLLAR__ [ I]  .UpdateValue ( Functions.ItoA (  (int) ( ACTUALHOUR ) ) + ":00"  ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 57;
                    POINTERPOS = (ushort) ( (POINTERPOS + 4) ) ; 
                    } 
                
                __context__.SourceCodeLine = 36;
                } 
            
            
            }
            
        private void GETOFFTIMES (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 64;
            POINTERPOS = (ushort) ( (Functions.Find( "offTimes" , MAIN_PROGRAM_RX__DOLLAR__ ) + 12) ) ; 
            __context__.SourceCodeLine = 66;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)7; 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 68;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( POINTERPOS ) , (int)( 1 ) ) == ",") ) || Functions.TestForTrue ( Functions.BoolToInt (Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( POINTERPOS ) , (int)( 1 ) ) == "]") )) ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 70;
                    POINTERPOS = (ushort) ( (POINTERPOS - 1) ) ; 
                    __context__.SourceCodeLine = 71;
                    ACTUALHOUR = (ushort) ( (Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( POINTERPOS ) , (int)( 1 ) ) ) - 1) ) ; 
                    __context__.SourceCodeLine = 72;
                    DAYSOFFTIMES__DOLLAR__ [ I]  .UpdateValue ( "0" + Functions.ItoA (  (int) ( ACTUALHOUR ) ) + ":00"  ) ; 
                    __context__.SourceCodeLine = 73;
                    POINTERPOS = (ushort) ( (POINTERPOS + 3) ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 77;
                    POINTERPOS = (ushort) ( (POINTERPOS - 1) ) ; 
                    __context__.SourceCodeLine = 78;
                    ACTUALHOUR = (ushort) ( (Functions.Atoi( Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( POINTERPOS ) , (int)( 2 ) ) ) - 1) ) ; 
                    __context__.SourceCodeLine = 79;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( ACTUALHOUR < 10 ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 81;
                        DAYSOFFTIMES__DOLLAR__ [ I]  .UpdateValue ( "0" + Functions.ItoA (  (int) ( ACTUALHOUR ) ) + ":00"  ) ; 
                        } 
                    
                    else 
                        { 
                        __context__.SourceCodeLine = 85;
                        DAYSOFFTIMES__DOLLAR__ [ I]  .UpdateValue ( Functions.ItoA (  (int) ( ACTUALHOUR ) ) + ":00"  ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 87;
                    POINTERPOS = (ushort) ( (POINTERPOS + 4) ) ; 
                    } 
                
                __context__.SourceCodeLine = 66;
                } 
            
            
            }
            
        object MAIN_PROGRAM_RX__DOLLAR___OnChange_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 94;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Mid( MAIN_PROGRAM_RX__DOLLAR__ , (int)( 3 ) , (int)( 8 ) ) == "dayNames"))  ) ) 
                    { 
                    __context__.SourceCodeLine = 96;
                    GETDAYSTATES (  __context__  ) ; 
                    __context__.SourceCodeLine = 97;
                    GETONTIMES (  __context__  ) ; 
                    __context__.SourceCodeLine = 98;
                    GETOFFTIMES (  __context__  ) ; 
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
        
        DAYSONFB = new InOutArray<DigitalOutput>( 7, this );
        for( uint i = 0; i < 7; i++ )
        {
            DAYSONFB[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( DAYSONFB__DigitalOutput__ + i, this );
            m_DigitalOutputList.Add( DAYSONFB__DigitalOutput__ + i, DAYSONFB[i+1] );
        }
        
        MAIN_PROGRAM_RX__DOLLAR__ = new Crestron.Logos.SplusObjects.StringInput( MAIN_PROGRAM_RX__DOLLAR____AnalogSerialInput__, 10000, this );
        m_StringInputList.Add( MAIN_PROGRAM_RX__DOLLAR____AnalogSerialInput__, MAIN_PROGRAM_RX__DOLLAR__ );
        
        DAYSONTIMES__DOLLAR__ = new InOutArray<StringOutput>( 7, this );
        for( uint i = 0; i < 7; i++ )
        {
            DAYSONTIMES__DOLLAR__[i+1] = new Crestron.Logos.SplusObjects.StringOutput( DAYSONTIMES__DOLLAR____AnalogSerialOutput__ + i, this );
            m_StringOutputList.Add( DAYSONTIMES__DOLLAR____AnalogSerialOutput__ + i, DAYSONTIMES__DOLLAR__[i+1] );
        }
        
        DAYSOFFTIMES__DOLLAR__ = new InOutArray<StringOutput>( 7, this );
        for( uint i = 0; i < 7; i++ )
        {
            DAYSOFFTIMES__DOLLAR__[i+1] = new Crestron.Logos.SplusObjects.StringOutput( DAYSOFFTIMES__DOLLAR____AnalogSerialOutput__ + i, this );
            m_StringOutputList.Add( DAYSOFFTIMES__DOLLAR____AnalogSerialOutput__ + i, DAYSOFFTIMES__DOLLAR__[i+1] );
        }
        
        
        MAIN_PROGRAM_RX__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( MAIN_PROGRAM_RX__DOLLAR___OnChange_0, false ) );
        
        _SplusNVRAM.PopulateCustomAttributeList( true );
        
        NVRAM = _SplusNVRAM;
        
    }
    
    public override void LogosSimplSharpInitialize()
    {
        
        
    }
    
    public UserModuleClass_BARCLAYS_SCHEDULE_DECODER ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}
    
    
    
    
    const uint MAIN_PROGRAM_RX__DOLLAR____AnalogSerialInput__ = 0;
    const uint DAYSONFB__DigitalOutput__ = 0;
    const uint DAYSONTIMES__DOLLAR____AnalogSerialOutput__ = 0;
    const uint DAYSOFFTIMES__DOLLAR____AnalogSerialOutput__ = 7;
    
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
