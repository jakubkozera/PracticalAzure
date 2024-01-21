import { BrowserRouter, Route, Switch } from "react-router-dom";
import AppInsightsProvider from "./AppInsightsProvider";
import { PageTwo as Contact } from "./pages/Contact";
import { HomePage } from "./pages/Home";

const App = () => (
  <BrowserRouter>
    <AppInsightsProvider connectionString="InstrumentationKey=96a6dea2-3c27-4535-9855-b186e3a8d576;IngestionEndpoint=https://northeurope-2.in.applicationinsights.azure.com/;LiveEndpoint=https://northeurope.livediagnostics.monitor.azure.com/">
      <Switch>
        <Route path="/" exact component={HomePage} />
        <Route path="/contact" exact component={Contact} />
      </Switch>
    </AppInsightsProvider>
  </BrowserRouter>
);

export default App;
