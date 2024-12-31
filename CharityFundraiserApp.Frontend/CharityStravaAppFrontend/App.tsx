/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 */

import React, {useEffect, useState} from 'react';
import type {PropsWithChildren} from 'react';
import {
  SafeAreaView,
  ScrollView,
  StatusBar,
  StyleSheet,
  Text,
  useColorScheme,
  View,
} from 'react-native';

import { Colors } from 'react-native/Libraries/NewAppScreen';

function HeroSection({ appTitle }: { appTitle: string }) {
  return (
    <View style={styles.container}>
      <Text style={styles.header}>Fetched App title: {appTitle}</Text>
    </View>
  )
}

function App(): React.JSX.Element {
  const [appTitle, setAppTitle] = useState('');
  
  const isDarkMode = useColorScheme() === 'dark';

  const backgroundStyle = {
    backgroundColor: isDarkMode ? Colors.darker : Colors.lighter,
  };

  useEffect(() => {
    void fetchAppTitle();
  }, []);
  
  async function fetchAppTitle() {
    try {
      const response = await fetch('http://localhost:5127/app-title');
      
      if (response.ok) {
        const text = await response.text();
        setAppTitle(text);
      } else {
        console.error('Failed to fetch app title:', response.status);
      }
    } catch (error) {
      console.error('Error fetching app title:', error);
    }
  }

  return (
    <SafeAreaView style={backgroundStyle}>
      <StatusBar
        barStyle={isDarkMode ? 'light-content' : 'dark-content'}
        backgroundColor={backgroundStyle.backgroundColor}
      />
      <ScrollView
        contentInsetAdjustmentBehavior="automatic"
        style={backgroundStyle}>
        <HeroSection appTitle={appTitle} />
        <View
          style={{
            backgroundColor: isDarkMode ? Colors.black : Colors.white,
          }}>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#f8f8f8',
    paddingVertical: 50
  },
  header: {
    fontSize: 48, 
    fontWeight: 'bold', 
    textAlign: 'center',
    color: '#333',
  },
});

export default App;
