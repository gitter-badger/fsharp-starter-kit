/**
 * F# Starter Kit (https://github.com/kriasoft/fsharp-starter-kit)
 *
 * Copyright © 2014-2016 Kriasoft, LLC. All rights reserved.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.txt file in the root directory of this source tree.
 */

import React from 'react';
import { createApp } from 'react-app';

import routes from './routes';

createApp({
  routes,
  container: document.getElementById('container'),
});
