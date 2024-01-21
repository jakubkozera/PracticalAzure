import { ReactPlugin } from "@microsoft/applicationinsights-react-js";
import { ApplicationInsights } from "@microsoft/applicationinsights-web";
import * as React from "react";
import { useEffect } from "react";
import { RouteComponentProps, withRouter } from "react-router-dom";
type AppInsightsProviderProps = {
  children?: React.ReactNode;
  connectionString: string;
} & RouteComponentProps;

const AppInsightsProvider = ({
  children,
  history,
  connectionString
}: AppInsightsProviderProps): JSX.Element => {
  useEffect(() => {
    var reactPlugin = new ReactPlugin();
    var appInsights = new ApplicationInsights({
      config: {
        connectionString,
        extensions: [reactPlugin],
        extensionConfig: {
          [reactPlugin.identifier]: { history: history },
        },
      },
    });

    appInsights.loadAppInsights();
  }, []);
  return <>{children}</>;
};

export default withRouter(AppInsightsProvider);
