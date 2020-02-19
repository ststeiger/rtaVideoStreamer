
namespace AudioRecorder
{
    
    
    class Program
    {
        
        
        static void Main(string[] args)
        {
            // https://github.com/filoe/cscore/issues/94
            TestAudioRecording();
                    
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 
        
        
        // https://ourcodeworld.com/articles/read/702/how-to-record-the-audio-from-the-sound-card-system-audio-with-c-using-naudio-in-winforms
        // https://stackoverflow.com/questions/18812224/c-sharp-recording-audio-from-soundcard
        static void TestAudioRecording()
        {
            // Define the output wav file of the recorded audio
            string outputFilePath = @"D:\username\Desktop\system_recorded_audio.wav";

            // Redefine the capturer instance with a new instance of the LoopbackCapture class
            NAudio.Wave.WasapiLoopbackCapture CaptureInstance = new NAudio.Wave.WasapiLoopbackCapture();

            // Redefine the audio writer instance with the given configuration
            NAudio.Wave.WaveFileWriter RecordedAudioWriter = new NAudio.Wave.WaveFileWriter(outputFilePath, CaptureInstance.WaveFormat);

            // When the capturer receives audio, start writing the buffer into the mentioned file
            CaptureInstance.DataAvailable += (s, a) =>
            {
                // Write buffer into the file of the writer instance
                RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
            };

            // When the Capturer Stops, dispose instances of the capturer and writer
            CaptureInstance.RecordingStopped += (s, a) =>
            {
                RecordedAudioWriter.Dispose();
                RecordedAudioWriter = null;
                CaptureInstance.Dispose();
            };

            // Start audio recording !
            CaptureInstance.StartRecording();
            
            
            System.Console.WriteLine(" --- Press any key to stop recording --- ");
            System.Console.ReadKey();
            CaptureInstance.StopRecording();
        }
    }
}
