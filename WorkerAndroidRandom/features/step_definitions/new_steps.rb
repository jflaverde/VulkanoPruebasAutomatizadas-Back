Given /^I start the app the first time$/ do
    #Touch on next of the first screen
    touch("android.support.v7.widget.AppCompatButton id:'btn_save'")
    #Touch on next of the second screen
    touch("android.support.v7.widget.AppCompatButton id:'btn_save'")
    touch("android.support.v7.widget.AppCompatButton id:'btn_save'")
    sleep 1        
    touch("android.support.v7.widget.AppCompatCheckedTextView text:'Disable crash reports'")
    touch("android.support.v7.widget.AppCompatButton id:'btn_save'")
    touch("android.support.v7.widget.AppCompatButton id:'btn_save'")
    sleep 1
    touch("android.support.v7.widget.AppCompatButton id:'button1'")
end

Given /^I start the app and press next$/ do
    touch("android.support.v7.widget.AppCompatButton id:'btn_save'")
end

And /^I choose a random currency$/ do
    touch(query("android.support.v7.widget.AppCompatCheckedTextView")[rand(4)])
end

And /^I enable the crash report sending$/ do
    touch("android.support.v7.widget.AppCompatCheckedTextView text:'Automatically send crash reports'")
end

And /^I disable the crash report sending$/ do
    touch("android.support.v7.widget.AppCompatCheckedTextView text:'Automatically send crash reports'")
end

And /^I close the new in this version pop up$/ do
    touch("android.support.v7.widget.AppCompatButton id:'button1'")    
end

Then /^I press create account$/ do    
    touch("android.support.design.widget.FloatingActionButton id:'fab_create_account'")
end

Then /^I press create subaccount$/ do    
    touch("android.support.design.widget.FloatingActionButton id:'fab_create_transaction'")
end

Then /^I press create transaction$/ do    
    touch("android.support.design.widget.FloatingActionButton id:'fab_create_transaction'")
end

Then /^I enter "([^\"]*)" into input "([^\"]*)"$/ do |text, id|
    enter_text("android.support.design.widget.TextInputLayout id:'#{id}'", "#{text}")    
end

Then /^I enter "([^\"]*)" into amount$/ do |text|
    enter_text("org.gnucash.android.ui.util.widget.CalculatorEditText id:'input_transaction_amount'", "#{text}")    
end

Then /^I enter "([^\"]*)" into description$/ do |text|
    enter_text("android.support.v7.widget.AppCompatAutoCompleteTextView id:'input_transaction_name'", "#{text}")    
end

Then /^I enter the account name$/ do    
    touch("android.support.design.widget.TextInputLayout id:'name_text_input_layout'")         
    enter_text("android.support.design.widget.TextInputLayout id:'name_text_input_layout'", "Account name")
end

Then /^I enter the account description$/ do    
    touch("android.support.design.widget.TextInputLayout id:'description_input_layout'")         
    enter_text("android.support.design.widget.TextInputLayout id:'description_input_layout'", "Account description")
end

Then /^I select "([^\"]*)" from the spinner "([^\"]*)"$/ do |item_identifier, spinner_identifier|
    touch("android.widget.Spinner * marked:'#{spinner_identifier}'")
    touch("android.support.v7.widget.AppCompatCheckedTextView text:'#{item_identifier}''")
end

Then /^I select a random element from the spinner "([^\"]*)"$/ do |spinner_identifier|    
    touch("android.widget.Spinner * marked:'#{spinner_identifier}'")
    elementId = query("android.support.v7.widget.AppCompatCheckedTextView", :text) [rand(0..query("android.support.v7.widget.AppCompatCheckedTextView", :text).size)]
    touch("android.support.v7.widget.AppCompatCheckedTextView text:'#{elementId}''")
end

And /^I hide the keyboard$/ do
    perform_action('hide_soft_keyboard') 
end

And /^I press save$/ do
    touch("android.support.v7.view.menu.ActionMenuItemView id:'menu_save'")         
end

And /^I wait (\d+) seconds$/ do |value|
    sleep value
end

And /^I wait 1 second$/ do
    sleep 1
end

Given /^I wait (\d+) seconds start$/ do |value|
    sleep value
end

Given /^I make (\d+) events with a waiting time of (\d+)/ do |touches, time|        
    srand(104114833480311360780032690444137393916);
    #puts "#{seed}"

    for i in 1..touches do        
        actionNumber = rand(6)
        puts "actionNumber: #{actionNumber}"
        
        if (actionNumber == 0)
            x = rand(1..96)
            y = rand(4..97)
            puts "Coordinate: [#{x}, #{y}]"
            puts "time: #{time}]" #Resolver problemas con time
            perform_action('click_on_screen', x, y) 
        elsif (actionNumber == 1)
            perform_action('swipe', 'left')                    
        elsif (actionNumber == 2)
            perform_action('swipe', 'right')
        elsif (actionNumber == 3) 
            queryResult = query("android.support.design.widget.TextInputLayout")
            queryResultSize = queryResult.size
            puts "queryResultSize: [#{queryResult.size}]"
            if (size > 0)                                
                enter_text(queryResult[rand(queryResultSize)], "sdknsefheifhie")    
            end
        elsif (actionNumber == 4)
            if isScrollable()
                scroll_up
            end
        else 
            if isScrollable()
                scroll_down
            end
        end            

        #sleep time        
    end    
end

def isScrollable(options={})
    begin        
        scroll_view_query_string = "android.widget.ScrollView index:0"
        list_view_query_string = "android.widget.AbsListView index:0"
        web_view_query_string = "android.webkit.WebView index:0"

        loop do
            if element_exists(scroll_view_query_string)
                return true
            elsif element_exists(list_view_query_string)
                return true
            elsif element_exists(web_view_query_string)
                return true
            else
                return false        
            end
        end        
    end
end    
    