// Copyright © 2014-2016 Kriasoft, LLC. All rights reserved.
// This source code is licensed under the MIT license found in the
// LICENSE.txt file in the root directory of this source tree.

open System
open System.IO
open System.Runtime.Serialization
open System.Runtime.Serialization.Json
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Logging

[<DataContract>]
type AssetsJS = {
  [<field: DataMember(Name = "js")>]
  js: string
}

[<DataContract>]
type Assets = {
  [<field: DataMember(Name = "main")>]
  main: AssetsJS
}

type Startup() =
  member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment, factory: ILoggerFactory) =
    factory.AddConsole(Microsoft.Extensions.Logging.LogLevel.Trace) |> ignore
    app.UseStaticFiles() |> ignore
    let assets =
      use stream = File.OpenRead(Path.Combine(env.WebRootPath, "./assets/assets.json"))
      let obj = (new DataContractJsonSerializer(typeof<Assets>)).ReadObject(stream)
      obj :?> Assets
    app.Run(fun ctx -> ctx.Response.WriteAsync(sprintf """<!doctype html>
<html lang="">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title>F# Starter Kit</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <link rel="stylesheet" href="https://cdn.rawgit.com/tleunen/react-mdl/master/extra/material.min.css">
    <link rel="stylesheet" href="https://cdn.rawgit.com/isagalaev/highlight.js/master/src/styles/default.css">
    <link rel="apple-touch-icon" href="apple-touch-icon.png">
  </head>
  <body>
    <div id="container"></div>
    <script src="https://cdn.rawgit.com/tleunen/react-mdl/master/extra/material.min.js"></script>
    <script src="%s"></script>
    <script>
      window.ga=function(){ga.q.push(arguments)};ga.q=[];ga.l=+new Date;
      ga('create','UA-XXXXX-Y','auto');ga('send','pageview')
    </script>
    <script src="https://www.google-analytics.com/analytics.js" async defer></script>
  </body>
</html>""" assets.main.js)) |> ignore

[<EntryPoint>]
let main argv =
  let cwd = Directory.GetCurrentDirectory()
  let web = if Path.GetFileName(cwd) = "server" then "../public" else "public"
  let host = new WebHostBuilder()
  host.UseContentRoot(cwd) |> ignore
  host.UseWebRoot(web) |> ignore
  host.UseKestrel() |> ignore
  host.UseIISIntegration() |> ignore
  host.UseStartup<Startup>() |> ignore
  let host = host.Build()
  host.Run() |> ignore
  0 // return an integer exit code
