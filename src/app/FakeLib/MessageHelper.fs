﻿[<AutoOpen>]
module Fake.MessageHelper

/// Waits for other applications to create a output files
/// if the timeout is reached an exception will be raised
let WaitForMessageFiles files timeOut =
    let files = Seq.cache files
    tracefn "Waiting for message files %A (Timeout: %A)" files timeOut

    let time = 
        waitFor 
            (fun _ -> allFilesExist files) 
            timeOut 
            100 
            (fun _ -> failwith "MessageFile timeout") 
    System.Threading.Thread.Sleep 100
    time
  
/// Waits for another application to create a output file
///   - if the timeout is reached an exception will be raised
let WaitForMessageFile file timeOut = WaitForMessageFiles [file] timeOut